namespace Flashcards
{
    public class Card
    {
        public int Id { get; }
        public string Term { get; }
        public string Definition { get; }

        public Card(int id, string term, string definition)
        {
            Id = id;
            Term = term;
            Definition = definition;
        }
    }
}