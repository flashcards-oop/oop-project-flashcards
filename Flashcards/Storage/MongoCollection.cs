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
            return new MongoCollection(collection.Name, collection.Id, collection.Cards.Select(card => card.Id).ToList());
        }

        [BsonConstructor("Name", "Id", "CardsId")]
        public MongoCollection(string name, string id, List<string> cards)
        {
            Name = name;
            Id = id;
            CardsId = cards;
        }
    }
}