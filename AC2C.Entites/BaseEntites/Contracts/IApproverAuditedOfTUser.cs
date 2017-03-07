using System;

namespace AC2C.Entites.BaseEntites.Contracts
{
    public interface IApproverOfTUserAudited : IApproverAudited
    {
        /// <summary>
        /// Reference to the last modifier user of this entity.
        /// </summary>
        Guid? LastApproverUserId { get; set; }

        string LastApproverUserIp { get; set; }
    }
}