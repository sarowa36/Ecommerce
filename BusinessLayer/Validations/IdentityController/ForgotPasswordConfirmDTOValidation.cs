using EntityLayer.DTOs.Controllers.IdentityController;
using FluentValidation;

namespace BusinessLayer.Validations.IdentityController
{
    public class ForgotPasswordConfirmDTOValidation:AbstractValidator<ForgotPasswordConfirmDTO>
    {
        public ForgotPasswordConfirmDTOValidation()
        {
            RuleFor(x => x.Token).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.NewPassword).NotEmpty().MinimumLength(8).MaximumLength(16);
            RuleFor(x => x.NewPasswordConfirm).Equal(x => x.NewPassword).WithMessage("'{PropertyName}' and '{ComparisonProperty}' are not equal.");
        }
    }
}
