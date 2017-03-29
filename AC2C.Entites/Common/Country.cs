using System.Collections.Generic;
using AC2C.Entites.BaseEntites;

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
