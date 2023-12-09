using EntityLayer.ViewModels.IdentityController;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
