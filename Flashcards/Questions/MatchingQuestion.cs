namespace Flashcards
{
    public class MatchingQuestion : Question
    {
        public string[] Terms { get; }
        public string[] Definitions { get; }

        public MatchingQuestion(string[] terms, string[] definitions)
        {
            Terms = terms;
            Definitions = definitions;
        }

        protected bool Equals(MatchingQuestion other)
        {
            return Equals(Terms, other.Terms) && Equals(Definitions, other.Definitions);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((MatchingQuestion) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Terms != null ? Terms.GetHashCode() : 0) * 397) ^ (Definitions != null ? Definitions.GetHashCode() : 0);
            }
        }
    }
}
