using DataAccessLayer.Base.Repositories.OrderItemRepositories;
using DataAccessLayer.Base.Repositories.OrderRefundRepositories;
using DataAccessLayer.Base.Repositories.OrderRepositories;
using DataAccessLayer.Repositories.OrderRepositories;
using EntityLayer.Entities;
using EntityLayer.Enum;
using Iyzipay.Model;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Base;
using ServiceLayer.Base.Services;
using System.Runtime.InteropServices;

namespace ServiceLayer.Services
{
    public class OrderRefundService : IOrderRefundService
    {
        readonly IOrderItemReadRepository _orderItemReadRepository;
        readonly IOrderItemWriteRepository _orderItemWriteRepository;
        readonly IOrderRefundWriteRepository _orderRefundWriteRepository;
        readonly IOrderRefundReadRepository _orderRefundReadRepository;
        readonly IOrderWriteRepository _orderWriteRepository;
        readonly IServiceErrorContainer _serviceErrorContainer;
        public OrderRefundService(IOrderItemReadRepository orderItemReadRepository,
            IOrderItemWriteRepository orderItemWriteRepository,
            IOrderRefundWriteRepository orderRefundWriteRepository,
            IOrderWriteRepository orderWriteRepository,
            IServiceErrorContainer serviceErrorContainer,
            IOrderRefundReadRepository orderRefundReadRepository)
        {
            _orderItemReadRepository = orderItemReadRepository;
            _orderItemWriteRepository = orderItemWriteRepository;
            _orderRefundWriteRepository = orderRefundWriteRepository;
            _orderWriteRepository = orderWriteRepository;
            _serviceErrorContainer = serviceErrorContainer;
            _orderRefundReadRepository = orderRefundReadRepository;
        }

        public async Task<OrderRefund> CreateRefund(List<int> orderItemIds, string message, string userId)
        {
            var refundItems = _orderItemReadRepository.GetAll().Where(x => orderItemIds.Contains(x.Id) && x.Order.UserId == userId).Include(x => x.Order).ToList();
            if(refundItems==null && refundItems.Count <= 0)
            {
                _serviceErrorContainer.AddModelOnlyError("Order Items not found");
                return default;
            }
            if(DateTime.Now-refundItems.First().Order.CreateDate>TimeSpan.FromDays(22)) {
                _serviceErrorContainer.AddModelOnlyError("Acceptable Refund time is passed");
                return default;
            }
            if (!refundItems.All(x => x.OrderItemStatus == OrderItemStatus.NotOnRefundProccess && x.Order.OrderStatus == OrderStatus.Delivered && x.OrderId == refundItems.First().OrderId)
                || refundItems.First().Order.OrderStatus!=OrderStatus.Delivered)
            {
                _serviceErrorContainer.AddError("ModelOnly", "You cant create refund request");
                return default;
            }
            refundItems.ForEach(x => x.OrderItemStatus = OrderItemStatus.OnRefundProccess);
            var refundOrder = new OrderRefund()
            {
                OrderRefundOrderItems = refundItems.Select(x => new EntityLayer.M2M.OrderRefundM2MOrderItem() { OrderItemId = x.Id, }).ToList(),
                TotalRefundAmount = refundItems.Sum(x => x.Quantity * x.Price),
                UserId = userId,
                UserMessage = message,
                OrderRefundStatus = OrderRefundStatus.WaitingApprove,
                OrderId = refundItems.First().OrderId,
            };
            await _orderRefundWriteRepository.CreateAsync(refundOrder);
            await _orderItemWriteRepository.UpdateRangeAsync(refundItems);
            await _orderWriteRepository.SaveChangesAsync();
            return refundOrder;
        }
        public async Task<List<OrderRefund>> GetUserRefunds(string userId, int? index = null, int? count = null)
        {
            var query = _orderRefundReadRepository.GetAll().Include(x => x.OrderRefundOrderItems).ThenInclude(x => x.OrderItem).Where(x => x.UserId == userId).AsQueryable();
            if (index != null && count != null)
                query = query.Skip((int)index).Take((int)count);
            return query.OrderByDescending(x => x.CreateDate).ToList();
        }
        public async Task<List<OrderRefund>> GetAllRefunds(OrderRefundStatus? status = null, int? index = null, int? count = null)
        {
            var query = _orderRefundReadRepository.GetAll().Include(x => x.OrderRefundOrderItems).ThenInclude(x => x.OrderItem).AsQueryable();
            if (status != null)
                query = query.Where(x => x.OrderRefundStatus == status);
            if (index != null && count != null)
                query = query.Skip((int)index).Take((int)count);
            return query.OrderByDescending(x => x.CreateDate).ToList();
        }
        public async Task<OrderRefund> GetOrderRefund(int id)
        {
            var orderRefund = _orderRefundReadRepository.GetAll().Include(x => x.Order).FirstOrDefault(x => x.Id == id);
            if (orderRefund != null)
            {
                return orderRefund;
            }
            else
            {
                _serviceErrorContainer.AddModelOnlyError("Order Refund not exist");
                return orderRefund;
            }
        }
        public async Task CancelRefund(int id, string userId)
        {
            var orderRefund = _orderRefundReadRepository.GetAll().Include(x => x.OrderRefundOrderItems).ThenInclude(x => x.OrderItem).FirstOrDefault(x => x.Id == id && x.UserId == userId);
            if (orderRefund == null)
            {
                _serviceErrorContainer.AddModelOnlyError("Refund Not Found Or Approved");
                return;
            }
            if (orderRefund.OrderRefundStatus != OrderRefundStatus.WaitingApprove)
            {
                _serviceErrorContainer.AddModelOnlyError("Refund cant cancel");
                return;
            }
            orderRefund.OrderRefundOrderItems.ForEach(x => x.OrderItem.OrderItemStatus = OrderItemStatus.NotOnRefundProccess);
            orderRefund.OrderRefundStatus = OrderRefundStatus.Canceled;
            await _orderRefundWriteRepository.UpdateAsync(orderRefund);
            await _orderRefundWriteRepository.SaveChangesAsync();
        }
        public async Task IgnoreRefund(int id)
        {
            var orderRefund = _orderRefundReadRepository.GetAll().Include(x => x.OrderRefundOrderItems).ThenInclude(x => x.OrderItem).FirstOrDefault(x => x.Id == id && (x.OrderRefundStatus == OrderRefundStatus.ApprovedAndWaitingDelivery || x.OrderRefundStatus == OrderRefundStatus.WaitingApprove));
            if (orderRefund != null)
            {
                orderRefund.OrderRefundOrderItems.ForEach(x => x.OrderItem.OrderItemStatus = OrderItemStatus.NotOnRefundProccess);
                orderRefund.OrderRefundStatus = OrderRefundStatus.Ignored;
                await _orderRefundWriteRepository.UpdateAsync(orderRefund);
                await _orderRefundWriteRepository.SaveChangesAsync();
            }
            else
            {
                _serviceErrorContainer.AddModelOnlyError("Order Refund not found");
            }
        }
        public async Task ApproveRefund(int id, string cargocode)
        {
            if (_orderRefundReadRepository.GetWhere(x => x.Id == id && x.OrderRefundStatus == OrderRefundStatus.WaitingApprove, out var orderRefund))
            {
                orderRefund.OrderRefundStatus = OrderRefundStatus.ApprovedAndWaitingDelivery;
                orderRefund.CargoCode = cargocode;
                await _orderRefundWriteRepository.UpdateAsync(orderRefund);
                await _orderRefundWriteRepository.SaveChangesAsync();
            }
            else
            {
                _serviceErrorContainer.AddModelOnlyError("Order Refund not found");
            }
        }
        public async Task AcceptRefund(OrderRefund orderRefund, Refund refund)
        {
            orderRefund.OrderRefundStatus = OrderRefundStatus.Accepted;
            orderRefund.RefundResult = refund;
            await _orderRefundWriteRepository.UpdateAsync(orderRefund);
            await _orderRefundWriteRepository.SaveChangesAsync();
        }

        public async Task UpdateSellerMessage(int id, string message)
        {
            if (_orderRefundReadRepository.GetWhere(x => x.Id == id, out var orderRefund))
            {
                orderRefund.SellerResponse = message;
                await _orderRefundWriteRepository.UpdateAsync(orderRefund);
                await _orderRefundWriteRepository.SaveChangesAsync();
            }
            else
            {
                _serviceErrorContainer.AddModelOnlyError("Order Refund Not Found");
            }
        }
    }
}
