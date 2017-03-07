namespace AC2C.Entites.BaseEntites.Contracts
{
    public interface ICreateionNetwork :  IHasCreationTime
    {
        string CreatorUserIp { get; set; }
    }
}