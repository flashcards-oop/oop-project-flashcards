using Flashcards;

namespace FlashcardsClient
{
    public class CheckedAnswer
    {
        public bool IsCorrect { get; set; }
        public IAnswer CorrectAnswer { get; set; }
    }
}