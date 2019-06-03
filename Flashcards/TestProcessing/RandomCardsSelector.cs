using System;
using System.Collections.Generic;

namespace Flashcards
{
    public class RandomCardsSelector : ICardsSelector
    {
        private readonly Random random = new Random();

        public IEnumerable<List<Card>> GetCardBunches(List<Card> cards, IEnumerable<int> bunchSizes)
        {
            foreach(var bunchSize in bunchSizes)
            {
                var chosenCards = new List<Card>();
                for (var i = 0; i < bunchSize; i++)
                    chosenCards.Add(cards[random.Next(cards.Count)]);
                yield return chosenCards;
            }
        }
    }
}
