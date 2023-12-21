using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.ViewModels.Anonym.ProductController
{
    public class GetProductValueVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<string> Images { get; set; }
        public string Description { get; set; }
    }
}
