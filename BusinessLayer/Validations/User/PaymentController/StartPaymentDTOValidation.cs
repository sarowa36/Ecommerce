using EntityLayer.DTOs.Areas.User.PaymentController;
using FluentValidation;

namespace BusinessLayer.Validations.User.PaymentController
{
    public class StartPaymentDTOValidation:AbstractValidator<StartPaymentDTO>
    {
        public StartPaymentDTOValidation()
        {
            RuleFor(x => x.SelectedAddressId).NotEmpty();
            RuleFor(x => x.TargetName).NotEmpty().MinimumLength(6).MaximumLength(20);
            RuleFor(x => x.TargetPhone).NotEmpty().MinimumLength(8).MaximumLength(12);
            RuleFor(x => x.TargetPhone).Matches("^[0-9]+$").WithMessage("{PropertyName} must only number");
        }
    }
}
