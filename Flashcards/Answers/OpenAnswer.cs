using MongoDB.Bson.Serialization.Attributes;

namespace Flashcards.Answers
{
    public class OpenAnswer : IAnswer
    {
        [BsonElement]
        public string Answer { get; }

		[BsonConstructor]
        public OpenAnswer(string answer)
        {
            Answer = answer;
        }

        public bool IsTheSameAs(IAnswer otherAnswer)
        {
            if (!(otherAnswer is OpenAnswer))
                return false;
            return Answer == ((OpenAnswer)otherAnswer).Answer;
        }

        protected bool Equals(OpenAnswer other)
        {
            return string.Equals(Answer, other.Answer);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return IsTheSameAs((OpenAnswer) obj);
        }

        public override int GetHashCode()
        {
            return Answer != null ? Answer.GetHashCode() : 0;
        }
    }
}
