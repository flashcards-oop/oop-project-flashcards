using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flashcards
{
    public interface IStorage
    {
        Task AddCard(Card card);
        Task<Card> FindCard(string id);
        IEnumerable<Card> GetAllCards();
        Task DeleteCard(string id);
        
        Task AddCollection(Collection collection);
        Task<Collection> FindCollection(string id);
        IEnumerable<Collection> GetAllCollections();
        Task DeleteCollection(string id);

        Task<List<Card>> GetCollectionCards(string id);
    }
}