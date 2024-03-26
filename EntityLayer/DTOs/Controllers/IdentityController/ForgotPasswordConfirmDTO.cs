namespace EntityLayer.DTOs.Controllers.IdentityController
{
    public class ForgotPasswordConfirmDTO
    {
        public string NewPassword { get; set; }
        public string NewPasswordConfirm { get; set; }
        public string Token { get; set; }
        public string UserId { get; set; }
    }
}
