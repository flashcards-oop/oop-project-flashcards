using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace Flashcards
{
    public class MongoAnswers
    {
        [BsonElement]
        public string Id { get; }
        [BsonElement]
        public IEnumerable<Answer> Answers { get; }

        [BsonConstructor("Id", "Answers")]
        // ReSharper disable once UnusedMember.Global
        public MongoAnswers(string id, IEnumerable<Answer> answers)
        {
            Id = id;
            Answers = answers;
        }

        public MongoAnswers(IEnumerable<Exercise> exercises)
        {
            Id = Guid.NewGuid().ToString();
            Answers = exercises.Select(exercise => exercise.Answer);
        }
    }
}