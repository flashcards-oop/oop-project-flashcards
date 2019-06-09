using Flashcards;

namespace FlashcardsApi.Models
{
    public class ExerciseDto
    {
        public string Id { get; }
        public IQuestion Question { get; }

        public ExerciseDto(string id, IQuestion question)
        {
            Id = id;
            Question = question;
        }
    }
}