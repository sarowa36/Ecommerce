using AutoMapper;
using EntityLayer.Entities.JsonCookieEntities;
using EntityLayer.ViewModels.CommonVM;

namespace Ecommerce.AutoMappers
{
    public class GeneralProfile:Profile
    {
        public GeneralProfile()
        {
            CreateMap<SelectedProductVariation, SelectedProductVariationVM>().ReverseMap();
            CreateMap<CookieSelectedProductVariation, SelectedProductVariationVM>().ReverseMap();
            CreateMap<CookieSelectedProductVariation, SelectedProductVariation>().ReverseMap();
        }
    }
}
