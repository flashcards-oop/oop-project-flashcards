using System;
using System.Collections.Generic;

namespace Flashcards
{
    public class NewTestBuilder
    {
        List<Card> cards;
        List<(IExerciseGenerator generator, int amount)> generationSettings = new List<(IExerciseGenerator generator, int amount)>();
        Random random = new Random();

        public NewTestBuilder(List<Card> cards)
        {
            this.cards = cards;
        }

        public NewTestBuilder WithGenerator(IExerciseGenerator generator, int amountOfQuestions)
        {
            generationSettings.Add((generator, amountOfQuestions));
            return this;
        }

        public IEnumerable<Exercise> Build()
        {
            foreach ((var generator, var amountOfQuesions) in generationSettings)
                for (var i = 0; i < amountOfQuesions; i++)
                    yield return generator.GenerateExerciseFrom(
                        ChooseCardsRandomly(generator.RequiredAmountOfCards));
        }

        // Should be smarter and transfered to another entity
        List<Card> ChooseCardsRandomly(int amount)
        {
            var chosenCards = new List<Card>();
            for (var i = 0; i < amount; i++)
                chosenCards.Add(cards[random.Next(cards.Count)]);
            return chosenCards;
        }
    }
}
