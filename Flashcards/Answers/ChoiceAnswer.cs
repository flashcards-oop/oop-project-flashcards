using MongoDB.Bson.Serialization.Attributes;

namespace Flashcards
{
    public class ChoiceAnswer : Answer
    {
        [BsonElement]
        public string Answer { get; }

        public ChoiceAnswer(string answer, string id) : base(id)
        {
            Id = id;
            Answer = answer;
        }

        public override bool IsTheSameAs(Answer otherAnswer)
        {
            if (!(otherAnswer is ChoiceAnswer))
                return false;
            return Answer == ((ChoiceAnswer) otherAnswer).Answer;
        }

        protected bool Equals(ChoiceAnswer other)
        {
            return string.Equals(Answer, other.Answer);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return IsTheSameAs((Answer) obj);
        }

        public override int GetHashCode()
        {
            return (Answer != null ? Answer.GetHashCode() : 0);
        }
    }
}
