namespace Flashcards
{
    public class ChoiceAnswer : Answer
    {
        public string Choice { get; set; }

        public override bool IsTheSameAs(Answer otherAnswer)
        {
            if (otherAnswer == null || otherAnswer as ChoiceAnswer == null)
                return false;
            return Choice == (otherAnswer as ChoiceAnswer).Choice;
        }
    }
}
