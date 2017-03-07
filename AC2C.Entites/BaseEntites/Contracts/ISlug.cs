using System.ComponentModel.DataAnnotations;

namespace AC2C.Entites.BaseEntites.Contracts
{
    public interface ISlug
    {
        [MaxLength(150)]
        string Slug { get; set; }
    }
}