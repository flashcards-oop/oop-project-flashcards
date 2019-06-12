using System.Collections.Generic;

namespace Flashcards.TestProcessing
{
    public interface ITestBuilder
    {
        ITestBuilder WithGenerator(IExerciseGenerator generator, int amount);
        IEnumerable<Exercise> Build();
    }
}
