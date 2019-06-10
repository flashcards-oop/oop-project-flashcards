using Flashcards;
using FlashcardsApi.Config;
using MongoDB.Driver;

namespace FlashcardsApi.Mongo
{
    public class MongoContext : IMongoContext
    {
        private readonly IMongoDatabase db;
        
        public MongoContext(MongoDbConfig config)
        {
            var client = new MongoClient(config.ConnectionString);
            db = client.GetDatabase("flashcards");
        }

        public IMongoCollection<Card> Cards => db.GetCollection<Card>("cards");
        public IMongoCollection<Collection> Collections => db.GetCollection<Collection>("collections");
        public IMongoCollection<Test> Tests => db.GetCollection<Test>("tests");
        public IMongoCollection<User> Users => db.GetCollection<User>("users");
    }
}