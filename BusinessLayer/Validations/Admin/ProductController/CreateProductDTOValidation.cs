using EntityLayer.DTOs.Areas.Admin.ProductController;
using FluentValidation;

namespace BusinessLayer.Validations.Admin.ProductController
{
    public class CreateProductDTOValidation:AbstractValidator<CreateProductDTO>
    {
        public CreateProductDTOValidation()
        {
            RuleFor(x=> x.Name).NotEmpty().MinimumLength(3).MaximumLength(45);
            RuleFor(x => x.Description).MaximumLength(1000);
            RuleFor(x => x.Price).NotEmpty().GreaterThan(1);
            RuleFor(x => x.Images).NotEmpty();
            RuleForEach(x=>x.Variation).ChildRules(child=>child.RuleFor(x=>x.Name).NotEmpty());
            RuleForEach(x => x.Variation).ChildRules(child => child.RuleFor(x => x.Value).NotEmpty());
        }
    }
}
