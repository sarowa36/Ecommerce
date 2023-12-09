using EntityLayer.ViewModels.IdentityController;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLayer.Validations.IdentityController
{
    public class RegisterVMValidation:AbstractValidator<RegisterVM>
    {
        public RegisterVMValidation()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(20);
            RuleFor(x => x.Surname).NotEmpty().MinimumLength(3).MaximumLength(20);
            RuleFor(x=>x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.PhoneNumber).NotEmpty().MinimumLength(11).MaximumLength(11);
            RuleFor(x=>x.PhoneNumber).Must(x=>!string.IsNullOrWhiteSpace(x) && Regex.IsMatch(x, "^[0-9]+$")).WithMessage("'{PropertyName}' must be numeric.");
            RuleFor(x=>x.Password).NotEmpty().MinimumLength(8).MaximumLength(16);
            RuleFor(x => x.PasswordConfirm).Equal(x => x.Password).WithMessage("'{PropertyName}' and '{ComparisonProperty}' are not equal.");
        }
    }
}
