using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Flashcards;
using Flashcards.Storages;
using MongoDB.Driver;

namespace FlashcardsApi.Mongo
{
    public class MongoCardStorage : IStorage
    {
        private readonly IMongoContext context;
        public MongoCardStorage(IMongoContext context)
        {
            this.context = context;
        }
        
        public async Task AddCard(Card card, CancellationToken token = default(CancellationToken))
        {
            await context.Cards.InsertOneAsync(card, cancellationToken: token);
        }

        public async Task UpdateCardsAwareness(IEnumerable<Guid> ids, int delta, CancellationToken token = default(CancellationToken))
        {
            var update = Builders<Card>.Update;
            await context.Cards.UpdateManyAsync(c => ids.Contains(c.Id), update.Inc(c => c.Awareness, delta), cancellationToken:token);
        }
        
        public async Task<Card> FindCard(Guid id, CancellationToken token = default(CancellationToken))
        {
            return await context.Cards.Find(c => c.Id == id).FirstOrDefaultAsync(token);
        }
        
        public async Task<IEnumerable<Card>> GetAllCards(CancellationToken token = default(CancellationToken))
        {
            return await context.Cards.Find(c => true).ToListAsync(token);
        }

        public async Task DeleteCard(Guid id, CancellationToken token = default(CancellationToken))
        {
            await context.Cards.FindOneAndDeleteAsync(c => c.Id == id, cancellationToken: token);
        }

        public async Task AddCollection(Collection collection, CancellationToken token = default(CancellationToken))
        {
            await context.Collections.InsertOneAsync(collection, cancellationToken: token);
        }
        
        public async Task<Collection> FindCollection(Guid id, CancellationToken token = default(CancellationToken))
        {
            return await context.Collections.Find(c => c.Id == id).FirstOrDefaultAsync(token);
        }

        public async Task<IEnumerable<Collection>> GetAllCollections(CancellationToken token = default(CancellationToken))
        {
            return await context.Collections.Find(c => true).ToListAsync(token);
        }
        
        public async Task DeleteCollection(Guid id, CancellationToken token = default(CancellationToken))
        {
            await context.Collections.FindOneAndDeleteAsync(c => c.Id == id, cancellationToken: token);
        }

        public async Task<List<Card>> GetCollectionCards(Guid id, CancellationToken token = default(CancellationToken))
        {
            return await context.Cards.Find(c => c.CollectionId == id).ToListAsync(token);
        }
    }
}