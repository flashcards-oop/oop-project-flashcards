using Flashcards;

namespace FlashcardsApi.Models
{
    public class ExerciseVerdictDto
    {
        public bool Correct { get; set; }
        public IAnswer CorrectAnswer { get; set; }

        public ExerciseVerdictDto(bool correct, IAnswer correctAnswer)
        {
            Correct = correct;
            CorrectAnswer = correctAnswer;
        }
    }
}