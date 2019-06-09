using MongoDB.Bson.Serialization.Attributes;

namespace Flashcards
{
    public class ChoiceAnswer : IAnswer
    {
        [BsonElement]
        public string Answer { get; }

		[BsonConstructor]
        public ChoiceAnswer(string answer)
        {
            Answer = answer;
        }

        public bool IsTheSameAs(IAnswer otherAnswer)
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
            return IsTheSameAs((ChoiceAnswer) obj);
        }

        public override int GetHashCode()
        {
            return Answer != null ? Answer.GetHashCode() : 0;
        }
    }
}
