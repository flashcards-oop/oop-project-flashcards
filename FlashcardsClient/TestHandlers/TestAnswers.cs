using System;
using System.Collections.Generic;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace FlashcardsClient
{
    public class TestAnswers
    {
        public Guid TestId { get; set; }
        public List<ExerciseAnswer> Answers { get; set; }
    }
}
