using EntityLayer.ViewModels.IdentityController;
using FluentValidation;

namespace BusinessLayer.Validations.IdentityController
{
    public class LoginVmValidation:AbstractValidator<LoginVM>
    {
        public LoginVmValidation()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8).MaximumLength(16);
        }
    }
}
