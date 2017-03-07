using System.ComponentModel.DataAnnotations;
using AC2C.Common.SerializationToolkit;
using AC2C.Entites.BaseEntites.Contracts;

namespace AC2C.Entites.BaseEntites
{
    [Serializable]
    public abstract class LanguegeEntity : LanguegeEntity<int>
    {
    }

    [Serializable]
    public abstract class LanguegeEntity<TPrimaryKey> : Entity<TPrimaryKey>, IHasLanguege
    {
        [Required]
        public int LangId { get; set; }

        [MaxLength(150)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
    }
}