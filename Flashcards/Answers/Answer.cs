namespace Flashcards
{
    public abstract class Answer
    {
        public string Id { get; set; }

        public Answer(string id)
        {
            Id = id;
        }

        public abstract bool IsTheSameAs(Answer other);
    }
}