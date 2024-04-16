using EntityLayer.DTOs.Areas.User.CustomerSaveController;
using FluentValidation;

namespace BusinessLayer.Validations.User.CustomerSaveController
{
    public class DeleteCustomerSaveItemDTOValidation:AbstractValidator<DeleteCustomerSaveItemDTO>
    {
        public DeleteCustomerSaveItemDTOValidation()
        {
            RuleFor(x=>x.SaveId).NotEmpty().GreaterThan(0);
            RuleFor(x => x.ProductId).NotEmpty().GreaterThan(0);
        }
    }
}
