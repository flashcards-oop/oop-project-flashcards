using System;
using System.Collections.Generic;
using System.Linq;

namespace Flashcards
{
    public class TestBuilder
    {
        List<Card> cards;
        List<(IExerciseGenerator generator, int amount)> generationSettings = new List<(IExerciseGenerator generator, int amount)>();
        Random random = new Random();
        ICardsSelector selector;

        public TestBuilder(List<Card> cards, ICardsSelector selector)
        {
            this.cards = cards;
            this.selector = selector;
        }

        public TestBuilder WithGenerator(IExerciseGenerator generator, int amountOfQuestions)
        {
            generationSettings.Add((generator, amountOfQuestions));
            return this;
        }

        public IEnumerable<Exercise> Build()
        {
            var bunchGenerators = GetCardBunchGenerators();
            var cardBunches = selector.GetCardBunches(
                cards, bunchGenerators.Select(generator => generator.RequiredAmountOfCards));
            
            foreach ((var generator, var cardBunch) in bunchGenerators.Zip(cardBunches, (gen, bunch) => (gen, bunch)))
                yield return generator.GenerateExerciseFrom(cardBunch);
        }

        IEnumerable<IExerciseGenerator> GetCardBunchGenerators()
        {
            foreach ((var generator, var amountOfQuesions) in generationSettings)
                for (var i = 0; i < amountOfQuesions; i++)
                    yield return generator;
        }
    }
}
