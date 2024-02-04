using EntityLayer.DTOs.Areas.User.AddressController;
using FluentValidation;

namespace BusinessLayer.Validations.User.AddressController
{
    public class CreateAddressDTOValidation:AbstractValidator<CreateAddressDTO>
    {
        public CreateAddressDTOValidation()
        {
            RuleFor(x=>x.Name).NotEmpty().MinimumLength(3).MaximumLength(32);
            RuleFor(x=>x.Zip).NotEmpty().MinimumLength(5).MaximumLength(5);
            RuleFor(x => x.DistrictId).NotEmpty();
            RuleFor(x => x.CityId).NotEmpty();
            RuleFor(x => x.Detail).NotEmpty().MinimumLength(6);
        }
    }
}
