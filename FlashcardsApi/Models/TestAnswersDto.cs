using System.Collections.Generic;
using Flashcards;

namespace FlashcardsApi.Models
{
    public class TestAnswersDto
    {
        public string TestId { get; set; }
        public List<Answer> Answers { get; set; }
    }
}