namespace EntityLayer.DTOs.Areas.User.OrderRefundController
{
    public class CreateOrderRefundDTO
    {
        public Dictionary<int,int> Items { get; set; }
        public string Message { get; set; }
    }
}
