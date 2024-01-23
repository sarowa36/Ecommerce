using EntityLayer.M2M;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.Entities
{
    public class OrderItem:IIntIdentity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        public List<OrderRefundOrderItem> OrderRefundOrderItems { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
