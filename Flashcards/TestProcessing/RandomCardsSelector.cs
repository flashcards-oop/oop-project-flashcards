using System;
using System.Collections.Generic;
using System.Threading;

namespace Flashcards.TestProcessing
{
    public class RandomCardsSelector : ICardsSelector
    {
        private static readonly ThreadLocal<Random> Random = new ThreadLocal<Random>(() => new Random());

        public IEnumerable<List<Card>> GetCardBunches(List<Card> cards, IEnumerable<int> bunchSizes)
        {
            if (cards.Count == 0)
                yield break;

            foreach(var bunchSize in bunchSizes)
            {
                var chosenCards = new List<Card>();
                for (var i = 0; i < bunchSize; i++)
                    chosenCards.Add(cards[Random.Value.Next(cards.Count)]);
                yield return chosenCards;
            }
        }
    }
}
