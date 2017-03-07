using System.ComponentModel.DataAnnotations;
using AC2C.Common.SerializationToolkit;
using AC2C.Entites.BaseEntites.Contracts;

namespace AC2C.Entites.BaseEntites
{
    [Serializable]
    public abstract class TitleSlugEntity<TPrimaryKey> : TitleEntity<TPrimaryKey>, ISlug
    {
        [MaxLength(150)]
        public string Slug { get; set; }
    }

    [Serializable]
    public abstract class TitleSlugEntity : TitleSlugEntity<int>
    {
    }
}