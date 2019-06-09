using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace Flashcards
{
    public class MatchingAnswer : IAnswer
    {
        [BsonElement]
        public readonly Dictionary<string, string> Matches;

		[BsonConstructor]
        public MatchingAnswer(Dictionary<string, string> matches)
        {
            Matches = matches;
        }

        public bool IsTheSameAs(IAnswer otherAnswer)
        {
            if (!(otherAnswer is MatchingAnswer))
                return false;
            var otherMatchingAnswer = (MatchingAnswer) otherAnswer;

            return Matches.Count == otherMatchingAnswer.Matches.Count &&
                   !Matches.Except(otherMatchingAnswer.Matches).Any();
        }


        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return IsTheSameAs((MatchingAnswer) obj);
        }

        public override int GetHashCode()
        {
            return (Matches != null ? Matches.GetHashCode() : 0);
        }
    }
}
