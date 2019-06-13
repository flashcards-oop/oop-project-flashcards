namespace Flashcards.Questions
{
    public class OpenAnswerQuestion : IQuestion
    {
        public string Definition { get; }

        public OpenAnswerQuestion(string definition)
        {
            Definition = definition;
        }

        protected bool Equals(OpenAnswerQuestion other)
        {
            return string.Equals(Definition, other.Definition);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((OpenAnswerQuestion) obj);
        }

        public override int GetHashCode()
        {
            return (Definition != null ? Definition.GetHashCode() : 0);
        }
    }
}
