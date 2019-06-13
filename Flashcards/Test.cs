using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable All

namespace Flashcards
{
    public class Test : IOwnedResource
    {
        [BsonId]
        public Guid Id { get; set; }
        [BsonElement]
        public string OwnerLogin { get; }

        [BsonElement]
        public readonly List<Exercise> Exercises;

        [BsonConstructor]
        [JsonConstructor]
        public Test(List<Exercise> exercises, string ownerLogin, Guid id)
        {
            Id = id;
            Exercises = exercises;
            OwnerLogin = ownerLogin;
        }
        
        public Test(List<Exercise> exercises, string ownerLogin)
        {
            Id = Guid.NewGuid();
            Exercises = exercises;
            OwnerLogin = ownerLogin;
        }
    }
}
