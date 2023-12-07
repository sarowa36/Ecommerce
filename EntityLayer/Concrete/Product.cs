using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Product : IIntIdentity, ISoftDeletable, ICreateDate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<string> Images { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
