using System.ComponentModel.DataAnnotations.Schema;
using AC2C.Entites.BaseEntites;

namespace AC2C.Entites.Common
{
    public class City : TitleEntity
    {
        public int ProvinceId { get; set; }

        [ForeignKey("ProvinceId")]
        public virtual Province Province { get; set; }
    }
}
