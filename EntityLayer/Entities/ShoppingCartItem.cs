using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class ShoppingCartItem :ICreateDate
    {
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public string UserId { get; set;}
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set;}
        public int Quantity { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
