using EntityLayer.Entities.JsonDbEntities;

namespace EntityLayer.Entities
{
    public class Product : IIntIdentity, ISoftDeletable, ICreateDate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<string> Images { get; set; }
        public List<ProductVariation>? Variation { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
