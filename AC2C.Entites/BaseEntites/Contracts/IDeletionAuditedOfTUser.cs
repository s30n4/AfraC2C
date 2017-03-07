using System;

namespace AC2C.Entites.BaseEntites.Contracts
{
    /// <summary>
    /// Adds navigation properties to <see cref="IDeletionAudited"/> interface for user.
    /// </summary>
    /// <typeparam name="TUser">Type of the user</typeparam>
    public interface IDeletionAudited<TUser> : IDeletionAudited
        where TUser : IEntity<Guid>
    {
        /// <summary>
        /// Reference to the deleter user of this entity.
        /// </summary>
        TUser DeleterUser { get; set; }
    }
}