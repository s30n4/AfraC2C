using System;
using AC2C.Common.SerializationToolkit;
using AC2C.Entites.BaseEntites.Contracts;

namespace AC2C.Entites.BaseEntites
{
    [Serializable]
    public abstract class SoftDeleteEntity : SoftDeleteEntity<int>
    {
    }

    [Serializable]
    public abstract class SoftDeleteEntity<TPrimaryKey> : Entity<TPrimaryKey>, ISoftDelete
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletionTime { get; set; }
        public Guid? DeleterUserId { get; set; }
    }
}