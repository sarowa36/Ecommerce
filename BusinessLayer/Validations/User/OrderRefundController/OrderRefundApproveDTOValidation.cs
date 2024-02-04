using EntityLayer.DTOs.Areas.Admin.OrderRefundController;
using FluentValidation;

namespace BusinessLayer.Validations.User.OrderRefundController
{
    public class OrderRefundApproveDTOValidation:AbstractValidator<OrderRefundApproveDTO>
    {
        public OrderRefundApproveDTOValidation()
        {
            RuleFor(x=>x.Id).NotEmpty();
            RuleFor(x=>x.CargoCode).NotEmpty().MinimumLength(6);
        }
    }
}
