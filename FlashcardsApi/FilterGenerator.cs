using Flashcards;
using FlashcardsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlashcardsApi
{
    public class FilterGenerator
    {
        List<IFilterConfigurator> configurators;

        Dictionary<string, Func<IComparable, IComparable, bool>> availableOptions = 
            new Dictionary<string, Func<IComparable, IComparable, bool>>
        {
            { "min", (min, val) => min.CompareTo(val) <= 0 },
            { "max", (max, val) => max.CompareTo(val) >= 0 },
            { "eq", (first, second) => first.CompareTo(second) == 0 }
        };

        public FilterGenerator(IEnumerable<IFilterConfigurator> configurators)
        {
            this.configurators = configurators.ToList();
        }

        public Func<IEnumerable<Card>, IEnumerable<Card>> GetFilter(FilterDto filterDto)
        {
            var subFilters = new List<Func<Card, bool>>();
            foreach (var configurator in configurators)
            {
                if (configurator.GetName() == filterDto.Name)
                {
                    foreach(var optName in filterDto.Options.Keys)
                    {
                        var constraint = configurator.ExtractConstraint(filterDto.Options[optName]);
                        Func<Card, IComparable> selector = configurator.GetMetrics;
                        Func<Card, bool> subFilter = card => availableOptions[optName](constraint, selector(card));
                        subFilters.Add(subFilter);
                    }
                }
            }
            return cards => cards
                    .Where(card => subFilters
                        .Select(f => f(card))
                        .All(res => res));
        }
    }
}
