using System.Collections.Generic;

namespace Flashcards.TestProcessing
{
    public interface ITestBuilderFactory
    {
        ITestBuilder GetBuilder(List<Card> cards, ICardsSelector selector);
    }
}
