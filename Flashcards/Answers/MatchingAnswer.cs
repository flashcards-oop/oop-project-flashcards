using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace Flashcards
{
    public class MatchingAnswer : Answer
    {
        [BsonElement]
        public readonly Dictionary<string, string> Matches;

        public MatchingAnswer(Dictionary<string, string> matches)
        {
            Matches = matches;
        }

        public override bool IsTheSameAs(Answer otherAnswer)
        {
            if (!(otherAnswer is MatchingAnswer))
                return false;
            var otherMatchingAnswer = (MatchingAnswer) otherAnswer;

            //if (!ContnainsSameDefinitionsAs(otherMatchingAnswer))
            //    return false;

            return Matches.Count == otherMatchingAnswer.Matches.Count &&
                   !Matches.Except(otherMatchingAnswer.Matches).Any();
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
            return (Matches != null ? Matches.GetHashCode() : 0);
        }

        //private bool ContnainsSameDefinitionsAs(MatchingAnswer otherMatchingAnswer)
        //{
        //    return Matches.Keys.All(key => otherMatchingAnswer.Matches.ContainsKey(key)) &&
        //        otherMatchingAnswer.Matches.Keys.All(key => Matches.ContainsKey(key));
        //}
    }
}
