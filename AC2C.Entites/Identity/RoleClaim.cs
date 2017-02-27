using AC2C.Entites.AuditableEntity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AC2C.Entites.Identity
{
   
    public class RoleClaim : IdentityRoleClaim<int>, IAuditableEntity
    {
        public virtual Role Role { get; set; }
    }
}