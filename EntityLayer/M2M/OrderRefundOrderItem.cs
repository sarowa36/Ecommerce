using EntityLayer.Entities;

namespace EntityLayer.M2M
{
    public class OrderRefundOrderItem
    {
        public int OrderRefundId { get; set; }
        public OrderRefund OrderRefund { get; set; }
        public int OrderItemId { get; set; }
        public OrderItem OrderItem { get; set; }
    }
}
