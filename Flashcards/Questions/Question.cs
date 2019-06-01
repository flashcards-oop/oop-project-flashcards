namespace Flashcards
{
    public abstract class Question
    {
        protected string Id { get; set; }

        protected Question(string id)
        {
            Id = id;
        }
    }
}