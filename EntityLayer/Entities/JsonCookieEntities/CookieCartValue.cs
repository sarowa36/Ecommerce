using EntityLayer.ViewModels.CommonVM;

namespace EntityLayer.Entities.JsonCookieEntities
{
    public class CookieCartValue
    {
        public string Id { get; set; }=Guid.NewGuid().ToString();
        public int ProductId { get; set; }
        public int Quantity{ get; set; }
        public List<CookieSelectedProductVariation> Variation { get; set; }
    }
}
