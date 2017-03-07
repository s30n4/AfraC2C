using System.ComponentModel.DataAnnotations;

namespace AC2C.Entites.BaseEntites.Contracts
{
    public interface IHasTitle
    {
        [MaxLength(75)]
        string Name { get; set; }

        [MaxLength(150)]
        string Title { get; set; }

        [MaxLength(500)]
        string Description { get; set; }
    }
}