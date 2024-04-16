using EntityLayer.DTOs.Areas.User.CustomerSaveController;
using FluentValidation;

namespace BusinessLayer.Validations.User.CustomerSaveController
{
    public class CreateCustomerSaveItemDTOValidation:AbstractValidator<CreateCustomerSaveItemDTO>
    {
        public CreateCustomerSaveItemDTOValidation()
        {
            RuleFor(x=>x.CustomerSaveId).NotEmpty();
            RuleFor(x => x.ProductId).NotEmpty();
        }
    }
}
