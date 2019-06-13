namespace Flashcards.Answers
{
    public interface IAnswer
    {
        bool IsTheSameAs(IAnswer other);
    }
}