using System.ComponentModel.DataAnnotations;
using AC2C.Common.SerializationToolkit;
using AC2C.Entites.BaseEntites.Contracts;

namespace AC2C.Entites.BaseEntites
{
    [Serializable]
    public abstract class TitleEntity : TitleEntity<int>
    {
    }

    [Serializable]
    public abstract class TitleEntity<TPrimaryKey> : Entity<TPrimaryKey>, IHasTitle
    {
        [Required]
        [MaxLength(75)]
        public string Name { get; set; }

        [MaxLength(150)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
    }
}