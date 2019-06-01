namespace Flashcards
{
    public abstract class Answer
    {
        public string Id { get; protected set; }

        protected Answer(string id)
        {
            Id = id;
        }

        public abstract bool IsTheSameAs(Answer other);
    }
}