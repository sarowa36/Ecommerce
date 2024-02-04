﻿namespace EntityLayer.ViewModels.Admin.OrderRefundController
{
    public class AdminOrderRefundItemVM
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public OrderItemStatus OrderItemStatus { get; set; }
    }
}