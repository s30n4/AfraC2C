using AC2C.Entites.BaseEntites.Contracts;

namespace AC2C.Entites.BaseEntites
{
    public class PassivableTitleEntity : PassivableTitleEntity<int>
    {
    }

    public class PassivableTitleEntity<TPrimaryKey> : TitleEntity<TPrimaryKey>, IPassivable
    {
        public bool IsActive { get; set; }

        public PassivableTitleEntity()
        {
            IsActive = true;
        }
    }
}