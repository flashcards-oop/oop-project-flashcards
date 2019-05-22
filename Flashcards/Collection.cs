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
        [BsonElement]
        public string OwnerLogin { get; protected set; }
        
        [BsonConstructor]
        public Collection(string name, string ownerLogin, string id = null)
        {
            Id = id ?? Guid.NewGuid().ToString();
            Name = name;
            OwnerLogin = ownerLogin;
        }

        protected Collection()
        {
            
        }
    }
}