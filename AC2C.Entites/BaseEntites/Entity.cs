using AC2C.Common.SerializationToolkit;
using AC2C.Entites.BaseEntites.Contracts;

namespace AC2C.Entites.BaseEntites
{
    /// <summary>
    ///     A shortcut of <see cref="Entity{TPrimaryKey}" /> for most used primary key type (<see cref="int" />).
    /// </summary>
    [Serializable]
    public abstract class Entity : Entity<int>, IEntity
    {
    }
}