using System.Collections.Generic;

namespace Flashcards.TestChecking
{
    public class TestAnswersDto
    {
        public string TestId { get; set; }
        public List<ExerciseAnswerDto> Answers { get; set; }
    }
}