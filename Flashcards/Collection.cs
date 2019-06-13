using System;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable All

namespace Flashcards
{
    public class Collection : IOwnedResource
    {
        [BsonId]
        public Guid Id { get; set; }
        [BsonElement]
        public string Name { get; }
        [BsonElement]
        public string OwnerLogin { get; }
        
        [BsonConstructor]
        [JsonConstructor]
        public Collection(string name, string ownerLogin, Guid id)
        {
            Id = id;
            Name = name;
            OwnerLogin = ownerLogin;
        }
        
        public Collection(string name, string ownerLogin)
        {
            Id = Guid.NewGuid();
            Name = name;
            OwnerLogin = ownerLogin;
        }

        protected Collection()
        {
            
        }
    }
}