using System;
using Flashcards.Answers;

namespace FlashcardsApi.Models
{
    public class ExerciseAnswerDto
    {
        public Guid Id { get; set; }
        public IAnswer Answer { get; set; }
    }
}