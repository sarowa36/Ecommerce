﻿using EntityLayer.ViewModels.Admin.ProductController;
using FluentValidation;

namespace BusinessLayer.Validations.Admin.ProductController
{
    public class CreateProductVMValidation:AbstractValidator<CreateProductVM>
    {
        public CreateProductVMValidation()
        {
            RuleFor(x=> x.Name).NotEmpty().MinimumLength(3).MaximumLength(45);
            RuleFor(x => x.Description).MaximumLength(1000);
            RuleFor(x => x.Price).NotEmpty().GreaterThan(1);
            RuleFor(x => x.Images).NotEmpty();
        }
    }
}
