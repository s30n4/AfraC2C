namespace AC2C.Dtos.Identity.Emails
{
    public class PasswordResetDto : EmailsBase
    {
        public int UserId { set; get; }
        public string Token { set; get; }
    }
}