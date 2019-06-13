using Flashcards;

namespace FlashcardsClient
{
    public class ExerciseVerdict
    {
        public bool Correct { get; set; }
        public IAnswer CorrectAnswer { get; set; }
    }
}