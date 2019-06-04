using Flashcards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashcardsApi
{
    public interface IFilterConfigurator
    {
        string GetName();
        IComparable GetMetrics(Card card);
        IComparable ExtractConstraint(object obj);
    }
}
