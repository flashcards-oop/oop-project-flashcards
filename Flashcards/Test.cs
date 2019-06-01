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
        public readonly IEnumerable<Exercise> Exercises;

        [BsonConstructor]
        public Test(IEnumerable<Exercise> exercises, string ownerLogin, string id = null)
        {
            Id = id ?? GuidGenerator.GenerateGuid();
            Exercises = exercises;
            OwnerLogin = ownerLogin;
        }
    }
}
