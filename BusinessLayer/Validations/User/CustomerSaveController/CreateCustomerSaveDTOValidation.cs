using EntityLayer.DTOs.Areas.User.CustomerSaveController;
using FluentValidation;

namespace BusinessLayer.Validations.User.CustomerSaveController
{
    public class CreateCustomerSaveDTOValidation:AbstractValidator<CreateCustomerSaveDTO>
    {
        public CreateCustomerSaveDTOValidation()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(16);
        }
    }
}
