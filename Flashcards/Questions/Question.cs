namespace Flashcards
{
    public abstract class Question
    {
        public string Id { get; protected set; }

        protected Question(string id)
        {
            Id = id;
        }
    }
}