namespace AC2C.Dtos.Identity.Settings
{
    public class DistributedSqlServerCacheOptions
    {
        public string ConnectionString { set; get; }
        public string TableName { set; get; }
        public string SchemaName { set; get; }
    }
}