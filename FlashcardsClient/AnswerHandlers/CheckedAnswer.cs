using Flashcards;

namespace FlashcardsClient
{
    public class CheckedAnswer
    {
        public bool Correct { get; set; }
        public IAnswer CorrectAnswer { get; set; }
    }
}