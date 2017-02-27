using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AC2C.Entites.AuditableEntity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AC2C.Entites.Identity
{

    public class User : IdentityUser<int, UserClaim, UserRole, UserLogin>, IAuditableEntity
    {
        public User()
        {
            UserUsedPasswords = new HashSet<UserUsedPassword>();
            UserTokens = new HashSet<UserToken>();
        }

        [StringLength(450)]
        public string FirstName { get; set; }

        [StringLength(450)]
        public string LastName { get; set; }

        [NotMapped]
        public string DisplayName
        {
            get
            {
                var displayName = $"{FirstName} {LastName}";
                return string.IsNullOrWhiteSpace(displayName) ? UserName : displayName;
            }
        }

        [StringLength(450)]
        public string PhotoFileName { get; set; }

        public DateTimeOffset? BirthDate { get; set; }

        public DateTimeOffset? CreatedDateTime { get; set; }

        public DateTimeOffset? LastVisitDateTime { get; set; }

        public bool IsEmailPublic { get; set; }

        public string Location { set; get; }

        public bool IsActive { get; set; } = true;

        public virtual ICollection<UserUsedPassword> UserUsedPasswords { get; set; }

        public virtual ICollection<UserToken> UserTokens { get; set; }
    }
}