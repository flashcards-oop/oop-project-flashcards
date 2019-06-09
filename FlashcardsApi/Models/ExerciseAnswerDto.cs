using Flashcards;

namespace FlashcardsApi.Models
{
    public class ExerciseAnswerDto
    {
        public string Id { get; set; }
        public IAnswer Answer { get; set; }
    }
}