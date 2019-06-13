using Flashcards;

namespace FlashcardsClient
{
    public class ExerciseQuestion
    {
        public string Id { get; set; }
        public IQuestion Question { get; set; }
    }
}