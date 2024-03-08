using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.Base
{
    public interface IRecursive<T> where T : class,IRecursive<T>
    {
        int? ParentId { get; set; }
        [ForeignKey(nameof(ParentId))]
        List<T> Childrens { get; set;}
    }
}
