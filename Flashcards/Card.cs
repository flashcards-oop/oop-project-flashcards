using System;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

// ReSharper disable All

namespace Flashcards
{
    public class Card : IOwnedResource
    {
        [BsonId]
        public Guid Id { get; set; }
        [BsonElement]
        public Guid CollectionId { get; }
        [BsonElement]
        public string Term { get; }
        [BsonElement]
        public string Definition { get; }
        [BsonElement]
        public int Awareness { get; }
        [BsonElement]
        public string OwnerLogin { get; }

        [BsonConstructor]
        [JsonConstructor]
        public Card(string term, string definition, string ownerLogin, Guid collectionId, 
            Guid id, int awareness = 0)
        {
            Id = id;
            CollectionId = collectionId;
            Term = term;
            Definition = definition;
            Awareness = awareness;
            OwnerLogin = ownerLogin;
        }
        
        public Card(string term, string definition, string ownerLogin, Guid collectionId, int awareness = 0)
        {
            Id = Guid.NewGuid();
            CollectionId = collectionId;
            Term = term;
            Definition = definition;
            Awareness = awareness;
            OwnerLogin = ownerLogin;
        }
    }
}
