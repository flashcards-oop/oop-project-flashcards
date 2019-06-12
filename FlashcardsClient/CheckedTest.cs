using System.Collections.Generic;

namespace FlashcardsClient
{
    public class CheckedTest
    {
        public List<Dictionary<string, CheckedAnswer>> CheckedAnswers { get; set; }
        public int CorrectAnswersCount { get; set; }
        public int WrongAnswersCount { get; set; }
    }
}