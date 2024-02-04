using EntityLayer.M2M;
using Iyzipay.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.Entities
{
    public class OrderRefund : IIntIdentity, ICreateDate
    {
        public int Id { get; set; }
        public decimal TotalRefundAmount { get; set; }
        public List<OrderRefundM2MOrderItem> OrderRefundOrderItems { get; set; }=new List<OrderRefundM2MOrderItem>();
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public string? UserMessage { get; set; }
        public string? SellerResponse { get; set; }
        public OrderRefundStatus OrderRefundStatus { get; set; }
        public string? CargoCode { get; set; }
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        public DateTime CreateDate { get; set; }
        public Refund? RefundResult { get; set; }
    }
}
