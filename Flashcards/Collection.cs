using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace Flashcards
{
    public class Collection
    {
        public string Id { get; protected set; }
        [BsonElement]
        public string Name { get; protected set; }
        [BsonIgnoreIfNull]
        public List<Card> Cards { get; }
        
        [BsonConstructor]
        public Collection(string name, string id = null, List<Card> cards = null)
        {
            Id = id ?? Guid.NewGuid().ToString();
            Name = name;
            Cards = cards ?? new List<Card>();
        }

        protected Collection()
        {
            
        }
    }
}