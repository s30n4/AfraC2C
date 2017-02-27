using AC2C.Entites.Identity;

namespace AC2C.Dtos.Identity.Emails
{
    public class UserProfileUpdateNotificationDto : EmailsBase
    {
        public User User { set; get; }
    }
}