// ReSharper disable MemberCanBePrivate.Global

namespace FlashcardsApi.Config
{
    public class MongoDbConfig
    {
        public string Host { get; set; }
        public int Port { get; set; }

        public string ConnectionString => $@"mongodb://{Host}:{Port}";
    }
}