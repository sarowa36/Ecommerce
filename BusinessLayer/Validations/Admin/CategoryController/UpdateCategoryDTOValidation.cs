using EntityLayer.DTOs.Areas.Admin.CategoryController;
using FluentValidation;

namespace BusinessLayer.Validations.Admin.CategoryController
{
    public class UpdateCategoryDTOValidation : AbstractValidator<UpdateCategoryDTO>
    {
        public UpdateCategoryDTOValidation()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
