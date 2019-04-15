namespace Flashcards
{
    public class ChoiceQuestion : Question
    {
        public string Definition { get; }
        public string[] Choices { get; }

        public ChoiceQuestion(string definition, string[] choises)
        {
            Definition = definition;
            Choices = choises;
        }

        protected bool Equals(ChoiceQuestion other)
        {
            return string.Equals(Definition, other.Definition) && Equals(Choices, other.Choices);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ChoiceQuestion) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Definition != null ? Definition.GetHashCode() : 0) * 397) ^ (Choices != null ? Choices.GetHashCode() : 0);
            }
        }
    }
}
