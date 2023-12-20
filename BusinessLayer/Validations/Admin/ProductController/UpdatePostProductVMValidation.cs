using EntityLayer.ViewModels.Admin.ProductController;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validations.Admin.ProductController
{
    public class UpdatePostProductVMValidation:AbstractValidator<UpdatePostProductVM>
    {
        public UpdatePostProductVMValidation()
        {
            RuleFor(x=>x.Id).NotEmpty().GreaterThan(0);
            RuleFor(x=>x.Price).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty().MinimumLength(3);
            RuleFor(x => x.Images).NotEmpty();
        }
    }
}
