using System.Collections.Generic;

namespace FlashcardsClient.Infrastructure
{
    public class TestAnswers
    {
        public string TestId { get; set; }
        public List<ExerciseAnswer> Answers { get; set; }
    }
}
