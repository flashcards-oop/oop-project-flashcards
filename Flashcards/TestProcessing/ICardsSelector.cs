using System.Collections.Generic;

namespace Flashcards
{
    public interface ICardsSelector
    {
        IEnumerable<List<Card>> GetCardBunches(List<Card> cards, IEnumerable<int> bunchSizes);
    }
}
