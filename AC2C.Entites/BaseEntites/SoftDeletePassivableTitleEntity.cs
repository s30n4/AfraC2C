using AC2C.Common.SerializationToolkit;
using AC2C.Entites.BaseEntites.Contracts;

namespace AC2C.Entites.BaseEntites
{
    [Serializable]
    public class SoftDeletePassivableTitleEntity<TPrimaryKey> : SoftDeleteTitleEntity<TPrimaryKey>, IPassivable
    {
        public bool IsActive { get; set; }
    }

    [Serializable]
    public class SoftDeletePassivableTitleEntity : SoftDeletePassivableTitleEntity<int>
    {
    }
}