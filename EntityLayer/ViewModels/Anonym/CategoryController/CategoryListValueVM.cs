using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.ViewModels.Anonym.CategoryController
{
    public class CategoryListValueVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public int SortIndex { get; set; }
        public List<CategoryListValueVM> Childrens { get; set; }
    }
}
