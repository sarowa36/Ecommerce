using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.Entities
{
    public class Category : IIntIdentity, ISoftDeletable,IRecursive<Category>,ISortable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        [ForeignKey(nameof(ParentId))]
        public List<Category> Childrens { get; set; }
        public bool IsDeleted { get; set; }
        public int SortIndex { get; set; }
    }
}
