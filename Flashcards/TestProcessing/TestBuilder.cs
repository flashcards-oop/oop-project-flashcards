using System.Collections.Generic;
using System.Linq;
using Flashcards.TestProcessing;

namespace Flashcards
{
    public class TestBuilder : ITestBuilder
    {
        private readonly List<Card> cards;

        private readonly List<(IExerciseGenerator generator, int amount)> generationSettings = 
            new List<(IExerciseGenerator generator, int amount)>();

        private readonly ICardsSelector selector;

        public TestBuilder(List<Card> cards, ICardsSelector selector)
        {
            this.cards = cards;
            this.selector = selector;
        }

        public ITestBuilder WithGenerator(IExerciseGenerator generator, int amountOfQuestions)
        {
            generationSettings.Add((generator, amountOfQuestions));
            return this;
        }

        public IEnumerable<Exercise> Build()
        {
            var bunchGenerators = GetCardBunchGenerators();
            var cardBunches = selector.GetCardBunches(
                cards, bunchGenerators.Select(generator => generator.RequiredAmountOfCards));
            
            foreach (var (generator, cardBunch) in bunchGenerators.Zip(cardBunches, (gen, bunch) => (gen, bunch)))
            {
                var exercise = generator.GenerateExerciseFrom(cardBunch);
                exercise.UsedCardsIds.AddRange(cardBunch.Select(card => card.Id));
                yield return exercise;
            }    
        }

        IEnumerable<IExerciseGenerator> GetCardBunchGenerators()
        {
            foreach (var (generator, amountOfQuestions) in generationSettings)
                for (var i = 0; i < amountOfQuestions; i++)
                    yield return generator;
        }
    }
}
