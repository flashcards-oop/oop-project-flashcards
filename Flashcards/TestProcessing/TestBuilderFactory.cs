using System.Collections.Generic;

namespace Flashcards.TestProcessing
{
    public class TestBuilderFactory : ITestBuilderFactory
    {
        public ITestBuilder GetBuilder(List<Card> cards, ICardsSelector selector)
        {
            return new TestBuilder(cards, selector);
        }
    }
}
