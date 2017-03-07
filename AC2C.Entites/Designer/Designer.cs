using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using AC2C.Entites.BaseEntites;
using AC2C.Entites.Common;
using AC2C.Entites.Identity;

namespace AC2C.Entites.Designer
{
    public class Designer : Entity
    {
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual ICollection<Skill> Skills { get; set; }


    }
}
