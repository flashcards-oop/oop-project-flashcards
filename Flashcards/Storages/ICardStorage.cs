using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flashcards
{
    public interface IStorage
    {
        void AddCard(Card card);
        Task<Card> FindCard(string id);
        IEnumerable<Card> GetAllCards();
        void DeleteCard(string id);
        
        void AddCollection(Collection collection);
        Task<Collection> FindCollection(string id);
        IEnumerable<Collection> GetAllCollections();
        void DeleteCollection(string id);

        Task<List<Card>> GetCollectionCards(string id);
    }
}