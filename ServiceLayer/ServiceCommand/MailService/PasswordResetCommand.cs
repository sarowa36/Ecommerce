namespace ServiceLayer.ServiceCommand.MailService
{
    public class PasswordResetCommand
    {
        public string Email { get; set; }
        public string UserId { get; set;}
        public string Token { get; set; }
    }
}
