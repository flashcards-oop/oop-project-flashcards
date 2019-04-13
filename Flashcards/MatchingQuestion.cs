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
    }
}
