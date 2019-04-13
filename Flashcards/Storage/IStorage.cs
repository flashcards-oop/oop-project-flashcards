using System.Collections.Generic;

namespace Flashcards
{
    public interface IStorage
    {
        void AddCard(Card card);
        Card GetCard(string id);
        IEnumerable<Card> GetAllCards();
        void DeleteCard(string id);
        
        void AddCollection(Collection collection);
        Collection GetCollection(string id);
        IEnumerable<Collection> GetAllCollections();
        void DeleteCollection(string id);

        void AddCardToCollection(string collectionId, string cardId);
        void RemoveCardFromCollection(string collectionId, string cardId);
    }
}