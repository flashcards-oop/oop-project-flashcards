using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Flashcards
{
    public class Card : IOwnedResource
    {
        public string Id { get; }
        [BsonElement]
        public string Term { get; }
        [BsonElement]
        public string Definition { get; }
        [BsonElement]
        public string OwnerLogin { get; }

        [BsonConstructor]
        public Card(string term, string definition, string ownerLogin, string id = null)
        {
            Id = id ?? Guid.NewGuid().ToString();
            Term = term;
            Definition = definition;
            OwnerLogin = ownerLogin;
        }
    }
}
