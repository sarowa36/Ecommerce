using EntityLayer.Entities;

namespace EntityLayer.ViewModels.User.OrderController
{
    public class UserOrderVM
    {
        public int Id { get; set; }
        public List<UserOrderItemVM> OrderItems { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal PaidPrice { get; set; }
        public string CargoCode { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime CreateDate { get; set; }
        public UserAddress Address { get; set; }
        public string TargetName { get; set; }
        public string TargetPhone { get; set; }
    }
}
