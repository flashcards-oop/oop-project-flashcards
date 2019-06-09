using System.Collections.Generic;
using System.Linq;

namespace FlashcardsApi.Models
{
    public class TestResultsDto
    {
        public int CorrectAnswers => Answers.Count(e => e.Value.Correct);
        public int WrongAnswers => Answers.Count(e => !e.Value.Correct);
        public readonly Dictionary<string, ExerciseVerdictDto> Answers = 
            new Dictionary<string, ExerciseVerdictDto>();
    }
}