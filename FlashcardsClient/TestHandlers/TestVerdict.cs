using System;
using System.Collections.Generic;
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace FlashcardsClient
{
    public class TestVerdict
    {
        public Dictionary<Guid, ExerciseVerdict> Answers { get; set; }
        public int CorrectAnswers { get; set; }
        public int WrongAnswers { get; set; }
    }
}