using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace Flashcards
{
    public class Exercise
    {
        [BsonId]
        public string Id { get; }
        [BsonElement]
        public readonly IAnswer Answer;
        [BsonElement]
        public readonly IQuestion Question;
        [BsonElement]
        public readonly List<string> UsedCardsIds;

        [BsonConstructor]
        public Exercise(string id, IAnswer answer, IQuestion question, List<string> usedCardsIds)
        {
            Id = id;
            Answer = answer;
            Question = question;
            UsedCardsIds = usedCardsIds;
        }

        public Exercise(IAnswer answer, IQuestion question)
        {
            Id = GuidGenerator.GenerateGuid();
            Answer = answer;
            Question = question;
            UsedCardsIds = new List<string>();
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
