using EntityLayer.ViewModels.CommonVM;

namespace EntityLayer.ViewModels.User.OrderRefundController
{
    public class UserOrderRefundItemVM
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public List<SelectedProductVariationVM> Variation { get; set; }
    }
}
