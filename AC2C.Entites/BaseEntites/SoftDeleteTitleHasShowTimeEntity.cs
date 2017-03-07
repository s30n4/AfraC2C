using System;
using AC2C.Common.SerializationToolkit;
using AC2C.Entites.BaseEntites.Contracts;

namespace AC2C.Entites.BaseEntites
{
    [Serializable]
    public class SoftDeleteTitleHasShowTimeEntity : SoftDeleteTitleHasShowTimeEntity<int>
    {
    }

    [Serializable]
    public abstract class SoftDeleteTitleHasShowTimeEntity<TPrimaryKey> : SoftDeleteTitleEntity<TPrimaryKey>, IHasShowTime
    {
        public DateTime? ShowEndDate { get; set; }

        public DateTime? ShowStartDate { get; set; }
    }
}
