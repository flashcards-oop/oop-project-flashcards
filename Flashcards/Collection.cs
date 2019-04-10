using System.Collections.Generic;

namespace Flashcards
{
    public class Collection
    {
        public int Id { get; }
        public string Name { get; }
        public List<Card> Cards { get; }
    }
}