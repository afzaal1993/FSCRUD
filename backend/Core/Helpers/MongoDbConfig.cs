namespace core.Helpers
{
    public class MongoDbConfig
    {
        public string Name { get; init; }
        public string Host { get; init; }
        public int Port { get; init; }
        public string Username { get; set; }
        public string Password { get; set; }
        //public string ConnectionString => $"mongodb://{Host}:{Port}";
        public string ConnectionString => $"mongodb://{Username}:{Password}@{Host}:{Port}";
    }
}
