using System.Collections.Generic;

namespace FlashcardsApi.Models
{
    public class TestAnswersDto
    {
        public string TestId { get; set; }
        public List<ExerciseAnswerDto> Answers { get; set; }
    }
}