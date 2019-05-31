using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace Flashcards
{
    public class Exercise
    {
        [BsonElement]
        public readonly Answer Answer;
        [BsonElement]
        public readonly Question Question;
        [BsonElement]
        public readonly List<string> UsedCardsIds;

        [BsonConstructor]
        public Exercise(Answer answer, Question question, List<string> usedCardsIds)
        {
            Answer = answer;
            Question = question;
            UsedCardsIds = new List<string>();
        }

        public Exercise(Answer answer, Question question)
        {
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
                return ((Answer != null ? Answer.GetHashCode() : 0) * 397) ^ (Question != null ? Question.GetHashCode() : 0);
            }
        }
    }
}
