using System.Collections.Generic;

namespace FlashcardsClient
{
    public class CheckedTest
    {
        public List<Dictionary<string, CheckedAnswer>> Answers { get; set; }
        public int CorrectAnswers { get; set; }
        public int WrongAnswers { get; set; }
    }
}