using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DTOs.Areas.Admin.CategoryController
{
    public class CreateCategoryDTO
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }
}
