using EntityLayer.Entities.JsonDbEntities;

namespace EntityLayer.ViewModels.Anonym.ProductController
{
    public class GetProductValueVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<string> Images { get; set; }
        public List<ProductVariation> Variation { get; set; }
        public int? CategoryId { get; set; }
        public string Description { get; set; }
    }
}
