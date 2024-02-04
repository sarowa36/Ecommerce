using EntityLayer.DTOs.Areas.User.OrderRefundController;
using FluentValidation;

namespace BusinessLayer.Validations.User.OrderRefundController
{
    public class OrderRefundCreateDTOValidation:AbstractValidator<OrderRefundCreateDTO>
    {
        public OrderRefundCreateDTOValidation()
        {
            RuleFor(x => x.Ids).NotEmpty();
            RuleFor(x=>x.Message).NotEmpty().MinimumLength(10).MaximumLength(150);
        }
    }
}
