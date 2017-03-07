using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using AC2C.Entites.BaseEntites;
using AC2C.Entites.Identity;

namespace AC2C.Entites.Common
{
    public class Province : TitleEntity
    {
        public virtual ICollection<City> Cities { get; set; }
        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }

        public Province()
        {
            Cities=new HashSet<City>();
        }

    }
}
