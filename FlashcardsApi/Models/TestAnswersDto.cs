using System;
using System.Collections.Generic;

namespace FlashcardsApi.Models
{
    public class TestAnswersDto
    {
        public Guid TestId { get; set; }
        public List<ExerciseAnswerDto> Answers { get; set; }
    }
}