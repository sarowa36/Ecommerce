using EntityLayer.DTOs.Controllers.IdentityController;
using FluentValidation;

namespace BusinessLayer.Validations.IdentityController
{
    public class LoginDTOValidation:AbstractValidator<LoginDTO>
    {
        public LoginDTOValidation()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8).MaximumLength(16);
        }
    }
}
