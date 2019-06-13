using System;
using System.Collections.Generic;
using System.Linq;
// ReSharper disable UnusedMember.Global

namespace FlashcardsApi.Models
{
    public class TestResultsDto
    {
        public int CorrectAnswers => Answers.Count(e => e.Value.Correct);
        public int WrongAnswers => Answers.Count(e => !e.Value.Correct);
        public readonly Dictionary<Guid, ExerciseVerdictDto> Answers = 
            new Dictionary<Guid, ExerciseVerdictDto>();
    }
}