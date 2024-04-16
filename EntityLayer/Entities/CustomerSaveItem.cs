using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.Entities
{
    public class CustomerSaveItem:IIntIdentity
    {
        public int Id { get; set; }
        public int CustomerSaveId { get; set; }
        [ForeignKey("CustomerSaveId")]
        public CustomerSave CustomerSave { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
