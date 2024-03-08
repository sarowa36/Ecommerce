using AutoMapper;
using EntityLayer.DTOs.Areas.Admin.CategoryController;
using EntityLayer.Entities;
using EntityLayer.ViewModels.Admin.CategoryController;

namespace Ecommerce.AutoMappers.Admin
{
    public class CategoryControllerProfile:Profile
    {
        public CategoryControllerProfile()
        {
            CreateMap<CreateCategoryDTO, Category>();
            CreateMap<UpdateCategoryDTO, Category>();
            CreateMap<Category, CategoryListValueVM>();
            CreateMap<Category, CategoryUpdateVM>();
        }
    }
}
