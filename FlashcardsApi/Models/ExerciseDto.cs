using System;
using Flashcards.Questions;

namespace FlashcardsApi.Models
{
    public class ExerciseDto
    {
        public Guid Id { get; }
        public IQuestion Question { get; }

        public ExerciseDto(Guid id, IQuestion question)
        {
            Id = id;
            Question = question;
        }
    }
}