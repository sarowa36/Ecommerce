namespace EntityLayer.Entities.JsonDbEntities
{
    public class ProductVariation
    {
        public string Id { get; set; }=Guid.NewGuid().ToString();
        public string Name { get; set; }
        public List<string> Value { get; set; }
    }
}
