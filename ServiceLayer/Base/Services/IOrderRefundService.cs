using EntityLayer.DTOs.Areas.User.OrderRefundController;
using EntityLayer.Entities;
using EntityLayer.Enum;
using Iyzipay.Model;

namespace ServiceLayer.Base.Services
{
    public interface IOrderRefundService
    {
        Task AcceptRefund(OrderRefund orderRefund,Refund refund);
        Task ApproveRefund(int id, string cargocode);
        Task CancelRefund(int id, string userId);
        Task<OrderRefund> CreateRefund(CreateOrderRefundDTO model, string userId);
        Task<List<OrderRefund>> GetUserRefunds(string userId, int? index = null, int? count = null);
        Task<List<OrderRefund>> GetAllRefunds(OrderRefundStatus? status = null, int? index = null, int? count = null);
        Task<OrderRefund> GetOrderRefund(int id);
        Task IgnoreRefund(int id);
        Task UpdateSellerMessage(int id, string message);
    }
}