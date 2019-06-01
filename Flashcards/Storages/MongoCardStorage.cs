using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Flashcards
{
    public class MongoCardStorage : IStorage
    {
        private readonly IMongoCollection<Card> cards;
        private readonly IMongoCollection<Collection> collections;


        public MongoCardStorage()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("flashcards");
            cards = database.GetCollection<Card>("cards");
            collections = database.GetCollection<Collection>("collections");
        }
        
        public async Task AddCard(Card card)
        {
            await cards.InsertOneAsync(card);
        }
        
        public async Task<Card> FindCard(string id)
        {
            return await cards.Find(c => c.Id == id).FirstOrDefaultAsync();
        }
        
        public async Task<IEnumerable<Card>> GetAllCards()
        {
            return await Task.FromResult(cards.AsQueryable());
        }

        public async Task DeleteCard(string id)
        {
            await cards.FindOneAndDeleteAsync(c => c.Id == id);
        }

        public async Task AddCollection(Collection collection)
        {
            await collections.InsertOneAsync(collection);
        }
        
        public async Task<Collection> FindCollection(string id)
        {
            return await collections.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Collection>> GetAllCollections()
        {
            return await Task.FromResult(collections.AsQueryable());
        }
        
        public async Task DeleteCollection(string id)
        {
            await collections.FindOneAndDeleteAsync(c => c.Id == id);
        }

        public async Task<List<Card>> GetCollectionCards(string id)
        {
            return await cards.Find(c => c.CollectionId == id).ToListAsync();
        }
    }
}