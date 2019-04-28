using MongoDB.Bson.Serialization.Attributes;

namespace Flashcards
{
    public class OpenAnswer : Answer
    {
        [BsonElement]
        public string Answer { get; }

        public OpenAnswer(string answer)
        {
            Answer = answer;
        }

        public override bool IsTheSameAs(Answer otherAnswer)
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
            if (obj.GetType() != this.GetType()) return false;
            return IsTheSameAs((Answer) obj);
        }

        public override int GetHashCode()
        {
            return (Answer != null ? Answer.GetHashCode() : 0);
        }
    }
}
