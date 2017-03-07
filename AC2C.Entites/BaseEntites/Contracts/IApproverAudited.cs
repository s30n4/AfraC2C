namespace AC2C.Entites.BaseEntites.Contracts
{
    public interface IApproverAudited : IHasApproveTime
    {
        bool? IsApproved { get; set; }
    }
}