using System.Collections.Generic;
using System.Linq;
using Flashcards;
using NUnit.Framework;

namespace FlashcardsTests
{
    [TestFixture]
    public class TestBuilderShould
    {
        private readonly List<Card> testCards = new List<Card>
        {
            new Card("1", "London", "is the capital of Great Britain"),
            new Card("666", "Moscow", "is the capital of Russian Federation"),
            new Card("13", "Mama", "Romama")
        };
        
        [Test]
        public void GenerateOpenAnswerTest()
        {
            var test = new TestBuilder(testCards, new RandomCardsSelector())
                .WithGenerator(new OpenQuestionExerciseGenerator(), 3)
                .Build();
            
            Assert.That(test.Count(), Is.EqualTo(3));
        }

        [Test]
        public void GenerateChoiceTest()
        {
            var test = new TestBuilder(testCards, new RandomCardsSelector())
                .WithGenerator(new ChoiceQuestionExerciseGenerator(), 3)
                .Build();
            
            Assert.That(test.Count(), Is.EqualTo(3));
        }

        [Test]
        public void GenerateMatchingTest()
        {
            var test = new TestBuilder(testCards, new RandomCardsSelector())
                .WithGenerator(new MatchingQuestionExerciseGenerator(), 3)
                .Build();
            
            Assert.That(test.Count(), Is.EqualTo(3));
        }
    }
}
