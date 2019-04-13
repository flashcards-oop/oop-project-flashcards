namespace Flashcards
{
    public class ChoiceAnswer : Answer
    {
        public string Answer { get; }

        public ChoiceAnswer(string answer)
        {
            Answer = answer;
        }

        public override bool IsTheSameAs(Answer otherAnswer)
        {
            if (!(otherAnswer is ChoiceAnswer))
                return false;
            return Answer == ((ChoiceAnswer) otherAnswer).Answer;
        }
    }
}
