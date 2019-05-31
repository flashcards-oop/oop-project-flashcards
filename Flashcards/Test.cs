using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace Flashcards
{
    public class Test : IOwnedResource
    {
        [BsonElement]
        public string Id { get; }
        [BsonElement]
        public string OwnerLogin { get; }

        [BsonElement]
        public IEnumerable<Exercise> exercises;

        [BsonConstructor]
        public Test(IEnumerable<Exercise> exercises, string ownerLogin, string id = null)
        {
            Id = id ?? Guid.NewGuid().ToString();
            this.exercises = exercises;
            OwnerLogin = ownerLogin;
        }
    }
}
