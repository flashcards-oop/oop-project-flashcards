using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Flashcards.Storages
{
    public interface IStorage
    {
        Task AddCard(Card card, CancellationToken token = default(CancellationToken));
        Task UpdateCardsAwareness(IEnumerable<Guid> ids, int delta, CancellationToken token = default(CancellationToken));
        Task<Card> FindCard(Guid id, CancellationToken token = default(CancellationToken));
        Task<IEnumerable<Card>> GetAllCards(CancellationToken token = default(CancellationToken));
        Task DeleteCard(Guid id, CancellationToken token = default(CancellationToken));
        
        Task AddCollection(Collection collection, CancellationToken token = default(CancellationToken));
        Task<Collection> FindCollection(Guid id, CancellationToken token = default(CancellationToken));
        Task<IEnumerable<Collection>> GetAllCollections(CancellationToken token = default(CancellationToken));
        Task DeleteCollection(Guid id, CancellationToken token = default(CancellationToken));

        Task<List<Card>> GetCollectionCards(Guid id, CancellationToken token = default(CancellationToken));
    }
}