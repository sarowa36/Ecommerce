using EntityLayer.Entities.JsonDbEntities;

namespace EntityLayer.ViewModels.Admin.ProductController
{
    public class UpdateProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<string> Images { get; set; }
        public List<ProductVariation> Variation { get; set; }
    }
}
