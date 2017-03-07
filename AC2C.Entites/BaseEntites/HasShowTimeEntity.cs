using System;
using AC2C.Common.SerializationToolkit;
using AC2C.Entites.BaseEntites.Contracts;

namespace AC2C.Entites.BaseEntites
{
    [Serializable]
    public abstract class HasShowTimeEntity : HasShowTimeEntity<int>
    {
    }

    [Serializable]
    public abstract class HasShowTimeEntity<TPrimaryKey> : Entity<TPrimaryKey>, IHasShowTime
    {
        public DateTime? ShowStartDate { get; set; }
        public DateTime? ShowEndDate { get; set; }
    }
}