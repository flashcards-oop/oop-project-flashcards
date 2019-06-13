using System;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

// ReSharper disable All

namespace Flashcards
{
    public class User
    {
        [BsonId]
        public Guid Id { get; set; }
        [BsonElement]
        public string Login { get; set; }
        
        [BsonConstructor]
        [JsonConstructor]
        public User(string login, Guid id)
        {
            Login = login;
            Id = id;
        }
        
        public User(string login)
        {
            Login = login;
            Id = Guid.NewGuid();
        }
    }
}
