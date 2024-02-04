using EntityLayer.ViewModels.CommonVM;

namespace EntityLayer.ViewModels.Anonym.AnonymShoppingCartController
{
    public class AnonymShoppingCartListValueVM
    {
        public string Id { get; set; }
        public int ProductId { get; set; }
        public string ProductImage { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        public List<SelectedProductVariationVM> SelectedVariation { get; set; }
    }
}
