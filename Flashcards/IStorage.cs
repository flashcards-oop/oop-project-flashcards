using System.Collections.Generic;

namespace Flashcards
{
    public interface IStorage
    {
        void Add(Card card);
        void Delete(int id);
        List<Collection> GetAllCollections();
        List<Card> GetAllCards();
        Collection GetCollection(int id);
        Card GetCard(int id);
    }
}