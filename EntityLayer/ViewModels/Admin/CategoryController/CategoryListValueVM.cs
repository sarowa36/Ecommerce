using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.ViewModels.Admin.CategoryController
{
    public class CategoryListValueVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CategoryListValueVM> Childrens { get; set; }
    }
}
