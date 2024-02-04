namespace EntityLayer.DTOs.Areas.User.PaymentController
{
    public class StartPaymentDTO
    {
        public int SelectedAddressId { get; set; }
        public string TargetName { get; set; }
        public string TargetPhone { get; set; }
    }
}
