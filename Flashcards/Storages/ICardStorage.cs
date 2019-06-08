using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Flashcards
{
    public interface IStorage
    {
        Task AddCard(Card card, CancellationToken token = default(CancellationToken));
        Task UpdateCardsAwareness(IEnumerable<string> ids, int delta, CancellationToken token = default(CancellationToken));
        Task<Card> FindCard(string id, CancellationToken token = default(CancellationToken));
        Task<IEnumerable<Card>> GetAllCards(CancellationToken token = default(CancellationToken));
        Task DeleteCard(string id, CancellationToken token = default(CancellationToken));
        
        Task AddCollection(Collection collection, CancellationToken token = default(CancellationToken));
        Task<Collection> FindCollection(string id, CancellationToken token = default(CancellationToken));
        Task<IEnumerable<Collection>> GetAllCollections(CancellationToken token = default(CancellationToken));
        Task DeleteCollection(string id, CancellationToken token = default(CancellationToken));

        Task<List<Card>> GetCollectionCards(string id, CancellationToken token = default(CancellationToken));
    }
}