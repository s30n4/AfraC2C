using AC2C.Entites.AuditableEntity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AC2C.Entites.Identity
{

    public class UserLogin : IdentityUserLogin<int>, IAuditableEntity
    {
        public virtual User User { get; set; }
    }
}