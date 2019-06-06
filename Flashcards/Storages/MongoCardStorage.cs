using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
        
        public async Task AddCard(Card card, CancellationToken token = default(CancellationToken))
        {
            await cards.InsertOneAsync(card, cancellationToken: token);
        }

        public async Task UpdateCardsAwareness(IEnumerable<string> ids, int delta, CancellationToken token = default(CancellationToken))
        {
            var update = Builders<Card>.Update;
            await cards.UpdateManyAsync(c => ids.Contains(c.Id), update.Inc(c => c.Awareness, delta), cancellationToken:token);
        }
        
        public async Task<Card> FindCard(string id, CancellationToken token = default(CancellationToken))
        {
            return await cards.Find(c => c.Id == id).FirstOrDefaultAsync(cancellationToken:token);
        }
        
        public async Task<IEnumerable<Card>> GetAllCards(CancellationToken token = default(CancellationToken))
        {
            return await cards.Find(c => true).ToListAsync(cancellationToken: token);
        }

        public async Task DeleteCard(string id, CancellationToken token = default(CancellationToken))
        {
            await cards.FindOneAndDeleteAsync(c => c.Id == id, cancellationToken: token);
        }

        public async Task AddCollection(Collection collection, CancellationToken token = default(CancellationToken))
        {
            await collections.InsertOneAsync(collection, cancellationToken: token);
        }
        
        public async Task<Collection> FindCollection(string id, CancellationToken token = default(CancellationToken))
        {
            return await collections.Find(c => c.Id == id).FirstOrDefaultAsync(cancellationToken: token);
        }

        public async Task<IEnumerable<Collection>> GetAllCollections(CancellationToken token = default(CancellationToken))
        {
            return await collections.Find(c => true).ToListAsync(cancellationToken: token);
        }
        
        public async Task DeleteCollection(string id, CancellationToken token = default(CancellationToken))
        {
            await collections.FindOneAndDeleteAsync(c => c.Id == id, cancellationToken: token);
        }

        public async Task<List<Card>> GetCollectionCards(string id, CancellationToken token = default(CancellationToken))
        {
            return await cards.Find(c => c.CollectionId == id).ToListAsync(cancellationToken: token);
        }
    }
}