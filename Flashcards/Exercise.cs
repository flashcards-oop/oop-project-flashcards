using System;
using System.Collections.Generic;
using Flashcards.Answers;
using Flashcards.Questions;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

// ReSharper disable All

namespace Flashcards
{
    public class Exercise
    {
        [BsonId]
        public Guid Id { get; set; }
        [BsonElement]
        public readonly IAnswer Answer;
        [BsonElement]
        public readonly IQuestion Question;
        [BsonElement]
        public readonly List<Guid> UsedCardsIds;

        [BsonConstructor]
        [JsonConstructor]
        public Exercise(Guid id, IAnswer answer, IQuestion question, List<Guid> usedCardsIds)
        {
            Id = id;
            Answer = answer;
            Question = question;
            UsedCardsIds = usedCardsIds;
        }

        public Exercise(IAnswer answer, IQuestion question)
        {
            Id = Guid.NewGuid();
            Answer = answer;
            Question = question;
            UsedCardsIds = new List<Guid>();
        }

        protected bool Equals(Exercise other)
        {
            return Equals(Answer, other.Answer) && Equals(Question, other.Question);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Exercise) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Answer != null ? Answer.GetHashCode() : 0) * 397) ^ 
                       (Question != null ? Question.GetHashCode() : 0);
            }
        }
    }
}
