using Flashcards;
using MongoDB.Driver;

namespace FlashcardsApi.Mongo
{
    public interface IMongoContext
    {
        IMongoCollection<Card> Cards { get; }
        IMongoCollection<Collection> Collections { get; }
        IMongoCollection<Test> Tests { get; }
        IMongoCollection<User> Users { get; }
    }
}