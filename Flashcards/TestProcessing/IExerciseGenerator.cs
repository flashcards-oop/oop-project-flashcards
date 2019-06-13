using System.Collections.Generic;

namespace Flashcards.TestProcessing
{
    public interface IExerciseGenerator
    {
        Exercise GenerateExerciseFrom(IList<Card> cards);
        int RequiredAmountOfCards { get; }
        string GetTypeCaption();
    }
}
