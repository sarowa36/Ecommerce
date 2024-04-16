using EntityLayer.DTOs.Areas.User.CustomerSaveController;
using FluentValidation;

namespace BusinessLayer.Validations.User.CustomerSaveController
{
    public class UpdateCustomerSaveDTOValidation:AbstractValidator<UpdateCustomerSaveDTO>
    {
        public UpdateCustomerSaveDTOValidation()
        {
            RuleFor(x=>x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(16);
        }
    }
}
