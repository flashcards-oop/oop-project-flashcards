using System.Collections.Generic;
using System.Linq;

namespace Flashcards.TestChecking
{
    public class TestResultsDto
    {
        public int CorrectAnswers => Answers.Count(e => e.Value.Correct);
        public int WrongAnswers => Answers.Count(e => !e.Value.Correct);
        public readonly Dictionary<string, ExerciseVerdictDto> Answers = 
            new Dictionary<string, ExerciseVerdictDto>();
    }
}