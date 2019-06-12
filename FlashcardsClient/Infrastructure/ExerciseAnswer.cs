using System.Collections.Generic;
using Flashcards;

namespace FlashcardsClient.Infrastructure
{
    public class ExerciseAnswer
    {
        public string Id { get; set; }
        public IAnswer Answer { get; set; }
    }

    public class TestAnswers
    {
        public string TestId { get; set; }
        public List<ExerciseAnswer> Answers { get; set; }
    }
}
