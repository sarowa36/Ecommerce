﻿namespace EntityLayer.ViewModels.Admin.ProductController
{
    public class UpdateGetProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<string> Images { get; set; }
    }
}
