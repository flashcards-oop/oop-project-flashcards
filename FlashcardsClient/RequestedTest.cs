using System.Collections.Generic;

namespace FlashcardsClient
{
    public class RequestedTest
    {
        public string TestId { get; set; }
        public List<ExerciseQuestion> Exercises { get; set; }
    }
}