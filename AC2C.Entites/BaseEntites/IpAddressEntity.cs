using System.ComponentModel.DataAnnotations;
using AC2C.Common.SerializationToolkit;

namespace AC2C.Entites.BaseEntites
{
    [Serializable]
    public abstract class IpAddressEntity : IpAddressEntity<int>
    {
    }

    [Serializable]
    public abstract class IpAddressEntity<TPrimaryKey> : Entity<TPrimaryKey>
    {
        [MaxLength(30)]
        public string IpAddress { get; set; }
    }
}