using AC2C.Entites.Identity;

namespace AC2C.Dtos.Identity.Emails
{
    public class RegisterEmailConfirmationDto : EmailsBase
    {
        public User User { set; get; }
        public string EmailConfirmationToken { set; get; }
    }
}
