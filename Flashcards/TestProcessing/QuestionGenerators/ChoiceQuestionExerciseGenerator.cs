using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Flashcards.QuestionGenerators
{
    public class ChoiceQuestionExerciseGenerator : IExerciseGenerator
    {
        private static readonly ThreadLocal<Random> random = new ThreadLocal<Random>(() => new Random());

        public ChoiceQuestionExerciseGenerator(int amountOfChoices = 4)
        {
            RequiredAmountOfCards = amountOfChoices;
        }

        public int RequiredAmountOfCards { get; }

        public string GetTypeCaption()
        {
            return "choice";
        }

        public Exercise GenerateExerciseFrom(IList<Card> cards)
        {
            if (cards.Count != RequiredAmountOfCards)
                throw new ArgumentException("Invalid amount of cards");

            var targetCard = cards[random.Value.Next(RequiredAmountOfCards)];
            var choices = cards.Select(card => card.Term).ToList();
            choices.Shuffle();

            return new Exercise(
                new ChoiceAnswer(targetCard.Term),
                new ChoiceQuestion(targetCard.Definition, choices.ToArray()));
        }
    }
}
