using EntityLayer.DTOs.Areas.User.CustomerSaveController;
using FluentValidation;

namespace BusinessLayer.Validations.User.CustomerSaveController
{
    public class DeleteCustomerSaveDTOValidation:AbstractValidator<DeleteCustomerSaveDTO>
    {
        public DeleteCustomerSaveDTOValidation()
        {
            RuleFor(x=>x.SaveId).NotEmpty().GreaterThan(0);
        }
    }
}
