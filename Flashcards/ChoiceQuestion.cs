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
    }
}
