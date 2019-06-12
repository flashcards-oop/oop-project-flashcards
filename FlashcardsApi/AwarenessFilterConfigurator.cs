using System;
using Flashcards;

namespace FlashcardsApi
{
    public class AwarenessFilterConfigurator : IFilterConfigurator
    {
        public IComparable ExtractConstraint(object obj)
        {
            return (int)((long)obj);
        }

        public string GetName()
        {
            return "awareness";
        }

        public IComparable GetMetrics(Card card)
        {
            return card.Awareness;
        }
    }
}
