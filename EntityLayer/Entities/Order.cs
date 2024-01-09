using Iyzipay.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.Entities
{
    public class Order : IIntIdentity, ICreateDate
    {
        public int Id { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal PaidPrice { get; set; }
        public CheckoutForm? PaymentResult { get; set; }
        public string? CargoCode { get; set; }
        public UserAddress? Address { get; set; }
        public string TargetName { get; set; }
        public string TargetPhone { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.PaymentFail;
        public DateTime CreateDate { get; set; }
    }
}
