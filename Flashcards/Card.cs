using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Flashcards
{
    public class Card
    {
        public string Id { get; }
        [BsonElement]
        public string Term { get; }
        [BsonElement]
        public string Definition { get; }

        [BsonConstructor]
        public Card(string term, string definition, string id = null)
        {
            Id = id ?? Guid.NewGuid().ToString();
            Term = term;
            Definition = definition;
        }
    }
}
