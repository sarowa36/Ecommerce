namespace EntityLayer.ViewModels.User.OrderRefundController
{
    public class UserOrderRefundVM
    {
        public int Id { get; set; }
        public List<UserOrderRefundItemVM> RefundItems { get; set; }
        public decimal TotalRefundAmount { get; set; }
        public DateTime CreateDate { get; set; }
        public OrderRefundStatus OrderRefundStatus { get; set; }
        public string UserMessage { get; set; }
        public string SellerResponse { get; set; }
        public string CargoCode { get; set; }
    }
}
