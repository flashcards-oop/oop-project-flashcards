using System;
using System.Collections.Generic;
using System.Linq;

namespace Flashcards
{
    public class MatchingQuestionExerciseGenerator : IExerciseGenerator
    {
        public MatchingQuestionExerciseGenerator(int matchesByQuestion = 3)
        {
            RequiredAmountOfCards = matchesByQuestion;
        }

        public int RequiredAmountOfCards { get; }

        public string GetTypeCaption()
        {
            return "matching";
        }

        public Exercise GenerateExerciseFrom(IList<Card> cards)
        {
            if (cards.Count != RequiredAmountOfCards)
                throw new ArgumentException("Invalid amount of cards");

            var terms = cards.Select(card => card.Term).ToList();
            var definitions = cards.Select(card => card.Definition).ToList();
            terms.Shuffle();
            definitions.Shuffle();

            var matches = new Dictionary<string, string>();
            foreach(var card in cards)
                matches[card.Definition] = card.Term;

            return new Exercise( 
                new MatchingAnswer(matches),
                new MatchingQuestion(terms.ToArray(), definitions.ToArray()));
        }
    }
}
