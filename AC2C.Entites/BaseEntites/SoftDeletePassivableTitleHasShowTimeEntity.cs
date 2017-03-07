using System;
using AC2C.Common.SerializationToolkit;
using AC2C.Entites.BaseEntites.Contracts;

namespace AC2C.Entites.BaseEntites
{
    [Serializable]
    public abstract class SoftDeletePassivableTitleHasShowTimeEntity : SoftDeletePassivableTitleHasShowTimeEntity<int> { }

    [Serializable]
    public abstract class SoftDeletePassivableTitleHasShowTimeEntity<TPrimaryKey> :
        SoftDeletePassivableTitleEntity<TPrimaryKey>, IHasShowTime
    {
        public DateTime? ShowStartDate { get; set; }
        public DateTime? ShowEndDate { get; set; }
    }
}