using System.Collections.Generic;

namespace Flashcards
{
    public interface IStorage
    {
        void AddCard(Card card);
        Card GetCard(string id);
        List<Card> GetAllCards();
        void DeleteCard(string id);
        
        void AddCollection(Collection collection);
        Collection GetCollection(string id);
        List<Collection> GetAllCollections();
        void DeleteCollection(string id);
    }
}