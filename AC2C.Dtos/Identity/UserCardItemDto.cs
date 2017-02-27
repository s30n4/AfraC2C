using System.Collections.Generic;
using AC2C.Entites.Identity;

namespace AC2C.Dtos.Identity
{
    public enum UserCardItemActiveTab
    {
        UserInfo,
        UserAdmin
    }

    public class UserCardItemDto
    {
        public User User { set; get; }
        public bool ShowAdminParts { set; get; }
        public List<Role> Roles { get; set; }
        public UserCardItemActiveTab ActiveTab { get; set; }
    }
}