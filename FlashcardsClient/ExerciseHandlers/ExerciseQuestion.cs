using System;
using Flashcards.Questions;

namespace FlashcardsClient
{
    public class ExerciseQuestion
    {
        public Guid Id { get; set; }
        public IQuestion Question { get; set; }
    }
}