using EntityLayer.DTOs.Controllers.IdentityController;
using FluentValidation;

namespace BusinessLayer.Validations.IdentityController
{
    public class ChangePasswordDTOValidation : AbstractValidator<ChangePasswordDTO>
    {
        public ChangePasswordDTOValidation()
        {
            RuleFor(x => x.OldPassword).NotEmpty().MinimumLength(8).MaximumLength(16);
            RuleFor(x => x.NewPassword).NotEmpty().MinimumLength(8).MaximumLength(16);
            RuleFor(x => x.NewPasswordConfirm).Equal(x => x.NewPassword).WithMessage("'{PropertyName}' and '{ComparisonProperty}' are not equal.");
        }
    }
}
