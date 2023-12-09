﻿using AutoMapper;
using EntityLayer.Concrete;
using EntityLayer.ViewModels.Admin.ProductController;

namespace Ecommerce.AutoMappers.Admin.ProductController
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, CreateProductVM>().ForMember(x=>x.Images,opt=>opt.Ignore());
            CreateMap<CreateProductVM,Product>().ForMember(x => x.Images, opt => opt.Ignore());
        }
    }
}