using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Flashcards
{
    public class Mongo : IStorage
    {
        private readonly IMongoCollection<Card> cards;
        private readonly IMongoCollection<Collection> collections;


        public Mongo()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("flashcards");
            cards = database.GetCollection<Card>("cards");
            collections = database.GetCollection<Collection>("collections");
        }
        
        public async void AddCard(Card card)
        {
            await cards.InsertOneAsync(card);
        }
        
        public async Task<Card> FindCard(string id)
        {
            return await cards.Find(c => c.Id == id).FirstOrDefaultAsync();
        }
        
        public IEnumerable<Card> GetAllCards()
        {
            return cards.AsQueryable();
        }

        public async void DeleteCard(string id)
        {
            await cards.FindOneAndDeleteAsync(c => c.Id == id);
        }

        public async void AddCollection(Collection collection)
        {
            await collections.InsertOneAsync(collection);
        }
        
        public async Task<Collection> FindCollection(string id)
        {
            return await collections.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public IEnumerable<Collection> GetAllCollections()
        {
            return collections.AsQueryable();
        }
        
        public async void DeleteCollection(string id)
        {
            await collections.FindOneAndDeleteAsync(c => c.Id == id);
        }

        public async Task<List<Card>> GetCollectionCards(string id)
        {
            return await cards.Find(c => c.CollectionId == id).ToListAsync();
        }
    }
}