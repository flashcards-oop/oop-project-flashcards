using System;
using System.Collections.Generic;
using System.Linq;
using Flashcards.Answers;

namespace Flashcards.TestProcessing
{
    public static class TestChecker
    {
        public static Dictionary<Guid, bool> Check(Dictionary<Guid, IAnswer> userAnswers, Test test)
        {
            return test.Exercises.ToDictionary(e => e.Id,
                e => userAnswers.ContainsKey(e.Id) && e.Answer.IsTheSameAs(userAnswers[e.Id]));
        }
    }
}