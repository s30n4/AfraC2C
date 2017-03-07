using System;

namespace AC2C.Entites.BaseEntites.Contracts
{
    public interface IHasApproveTime
    {
        /// <summary>
        /// Creation time of this entity.
        /// </summary>
        DateTime? ApproveTime { get; set; }
    }
}