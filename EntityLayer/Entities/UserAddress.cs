using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.Entities
{
    public class UserAddress:IIntIdentity,ISoftDeletable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public int Zip { get; set; }
        public string Detail { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User {  get; set; }
        public bool IsDeleted { get; set; }
    }
}
