namespace Flashcards
{
    public abstract class Question
    {
        public string Id { get; set; }

        public Question(string id)
        {
            Id = id;
        }
    }
}