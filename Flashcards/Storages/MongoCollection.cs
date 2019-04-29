using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace Flashcards
{
    public class MongoCollection : Collection
    {
        [BsonElement]
        public List<string> CardsId { get; }

        public static MongoCollection FromCollection(Collection collection)
        {
            return new MongoCollection(collection.Name, collection.Id, 
                collection.OwnerLogin, collection.Cards.Select(card => card.Id).ToList());
        }

        [BsonConstructor("Name", "Id", "OwnerLogin", "CardsId")]
        private MongoCollection(string name, string id, string ownerId, List<string> cards)
        {
            Name = name;
            Id = id;
            OwnerLogin = ownerId;
            CardsId = cards;
        }
    }
}