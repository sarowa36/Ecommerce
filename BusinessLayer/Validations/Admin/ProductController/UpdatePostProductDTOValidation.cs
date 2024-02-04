using EntityLayer.DTOs.Areas.Admin.ProductController;
using FluentValidation;

namespace BusinessLayer.Validations.Admin.ProductController
{
    public class UpdatePostProductDTOValidation:AbstractValidator<UpdateProductDTO>
    {
        public UpdatePostProductDTOValidation()
        {
            RuleFor(x=>x.Id).NotEmpty().GreaterThan(0);
            RuleFor(x=>x.Price).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty().MinimumLength(3);
            RuleFor(x => x.Images).NotEmpty();
        }
    }
}
