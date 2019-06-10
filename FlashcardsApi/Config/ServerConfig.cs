// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
namespace FlashcardsApi.Config
{
    public class ServerConfig
    {
        public MongoDbConfig MongoDb { get; set; } = new MongoDbConfig();
    }
}