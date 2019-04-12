namespace Flashcards
{
    public class ChoiceAnswer : Answer
    {
        public string Choice { get; set; }

        public override bool IsTheSameAs(Answer otherAnswer)
        {
            if (!(otherAnswer is ChoiceAnswer))
                return false;
            return Choice == ((ChoiceAnswer) otherAnswer).Choice;
        }
    }
}
