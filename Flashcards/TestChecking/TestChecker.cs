using System.Linq;

namespace Flashcards.TestChecking
{
    public static class TestChecker
    {
        public static TestResultsDto Check(TestAnswersDto answersDto, Test test)
        {
            var results = new TestResultsDto();

            foreach (var exercise in test.Exercises)
            {
                var userAnswer = answersDto.Answers.FirstOrDefault(a => a.Id == exercise.Id);
                var isCorrect = userAnswer?.Answer.IsTheSameAs(exercise.Answer) ?? false;
                results.Answers.Add(exercise.Id, new ExerciseVerdictDto(isCorrect, exercise.Answer));
            }

            return results;
        }
    }
}