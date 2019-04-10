namespace Flashcards
{
    public abstract class Answer
    {
        public int Id { get; set; }

        public abstract bool IsTheSameAs(Answer other);
    }
}