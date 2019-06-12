using System.Collections.Generic;
using Flashcards;

namespace FlashcardsClient
{
    public class ExerciseQuestion
    {
        public string Id { get; set; }
        public IQuestion Question { get; set; }
    }

    public class RequestedTest
    {
        public string TestId { get; set; }
        public List<ExerciseQuestion> Exercises { get; set; }
    }
}