using EntityLayer.M2M;
using EntityLayer.ViewModels.CommonVM;
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
        public List<OrderRefundM2MOrderItem> OrderRefundOrderItems { get; set; }
        public OrderItemStatus OrderItemStatus { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public List<SelectedProductVariation> Variation { get; set; }
    }
}
