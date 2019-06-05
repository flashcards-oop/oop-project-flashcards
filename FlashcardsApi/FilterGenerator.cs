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
            var configurator = configurators.Where(c => c.GetName() == filterDto.Name).FirstOrDefault();
            if (configurator == null)
                return cards => cards;

            var subFilters = ExtractSubfilters(configurator, filterDto.Options);
            return cards => cards.Where(card => subFilters.Select(subFilter => subFilter(card)).All(res => res));
        }

        private List<Func<Card, bool>> ExtractSubfilters(IFilterConfigurator configurator, Dictionary<string, object> filterOptions)
        {
            var subFilters = new List<Func<Card, bool>>();

            foreach (var optName in filterOptions.Keys.Intersect(availableOptions.Keys))
            {
                var constraint = configurator.ExtractConstraint(filterOptions[optName]);
                Func<Card, IComparable> selector = configurator.GetMetrics;
                Func<Card, bool> subFilter = card => availableOptions[optName](constraint, selector(card));
                subFilters.Add(subFilter);
            }

            return subFilters;
        }
    }
}
