using System;
using Flashcards.Answers;

namespace FlashcardsClient
{
    public class ExerciseAnswer
    {
        public Guid Id { get; set; }
        public IAnswer Answer { get; set; }
    }
}