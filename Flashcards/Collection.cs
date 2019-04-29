using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace Flashcards
{
    public class Collection : IOwnedResource
    {
        public string Id { get; protected set; }
        [BsonElement]
        public string Name { get; protected set; }
        [BsonIgnoreIfNull]
        public List<Card> Cards { get; }
        [BsonElement]
        public string OwnerLogin { get; protected set; }
        
        [BsonConstructor]
        public Collection(string name, string ownerLogin, string id = null, List<Card> cards = null)
        {
            Id = id ?? Guid.NewGuid().ToString();
            Name = name;
            OwnerLogin = ownerLogin;
            Cards = cards ?? new List<Card>();
        }

        protected Collection()
        {
            
        }
    }
}