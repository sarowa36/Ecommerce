using EntityLayer.DTOs.CommonDTOs;
using EntityLayer.Entities.JsonDbEntities;

namespace EntityLayer.DTOs.Areas.Admin.ProductController
{
    public class CreateProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int? CategoryId { get; set; }
        public List<FormFileAndLinkDTO> Images { get; set; }
        public List<ProductVariation> Variation { get; set; }
    }
}
