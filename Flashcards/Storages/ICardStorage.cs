using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flashcards
{
    public interface IStorage
    {
        Task AddCard(Card card);
        Task UpdateCardsAwareness(IEnumerable<string> ids, int delta);
        Task<Card> FindCard(string id);
        Task<IEnumerable<Card>> GetAllCards();
        Task DeleteCard(string id);
        
        Task AddCollection(Collection collection);
        Task<Collection> FindCollection(string id);
        Task<IEnumerable<Collection>> GetAllCollections();
        Task DeleteCollection(string id);

        Task<List<Card>> GetCollectionCards(string id);
    }
}