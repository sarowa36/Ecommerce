using EntityLayer.DTOs.Areas.Admin.CategoryController;
using FluentValidation;

namespace BusinessLayer.Validations.Admin.CategoryController
{
    public class CreateCategoryDTOValidation:AbstractValidator<CreateCategoryDTO>
    {
        public CreateCategoryDTOValidation()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
