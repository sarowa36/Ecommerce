using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.Entities
{
    public class CustomerSave:IIntIdentity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public List<CustomerSaveItem> Items { get; set; }
    }
}
