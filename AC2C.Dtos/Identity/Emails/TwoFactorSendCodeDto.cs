namespace AC2C.Dtos.Identity.Emails
{
    public class TwoFactorSendCodeDto : EmailsBase
    {
        public string Token { set; get; }
    }
}