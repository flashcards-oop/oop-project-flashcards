using System.Collections.Generic;
using System.Linq;

namespace Flashcards
{
    public static class TestChecker
    {
        public static Dictionary<string, bool> Check(Dictionary<string, IAnswer> userAnswers, Test test)
        {
            return test.Exercises.ToDictionary(e => e.Id,
                e => userAnswers.ContainsKey(e.Id) && e.Answer.IsTheSameAs(userAnswers[e.Id]));
        }
    }
}