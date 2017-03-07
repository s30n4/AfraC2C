using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AC2C.Entites.BaseEntites;
using AC2C.Entites.Identity;

namespace AC2C.Entites.Common
{
    public class Country : TitleEntity
    {
        public virtual ICollection<Province> Provinces { get; set; }

        public Country()
        {
            Provinces=new HashSet<Province>();
        }
    }
}
