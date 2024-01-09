﻿using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.Entities
{
    public class OrderItem:IIntIdentity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
