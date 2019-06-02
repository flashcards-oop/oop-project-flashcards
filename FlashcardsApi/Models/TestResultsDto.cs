using System;
using System.Collections.Generic;
using Flashcards;

namespace FlashcardsApi.Models
{
    public class TestResultsDto
    {
        public int CorrectAnswers { get; set; }
        public int WrongAnswers { get; set; }
        public Dictionary<string, Tuple<bool, Answer>> Answers { get; }

        public TestResultsDto()
        {
            Answers = new Dictionary<string, Tuple<bool, Answer>>();
            CorrectAnswers = 0;
            WrongAnswers = 0;
        }
    }
}