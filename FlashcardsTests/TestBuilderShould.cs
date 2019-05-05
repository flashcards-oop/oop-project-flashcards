using System.Collections.Generic;
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
        /* does not work
        [Test]
        public void GenerateMatchingTest()
        {
            var testBuilder = new ObsoleteTestBuilder(testCards);
            testBuilder.GenerateTasks(1, typeof(MatchingQuestion));
            var test = testBuilder.Build();
            var answer = new Dictionary<string, string>
            {
                ["London"] = "is the capital of Great Britain",
                ["Moscow"] = "is the capital of Russian Federation",
                ["Mama"] = "Romama"
            };
            
            Assert.That(test.Count, Is.EqualTo(1));
            var question = (MatchingQuestion) test[0].Question;
            Assert.That(question.Terms, Is.EquivalentTo(answer.Keys));
            Assert.That(question.Definitions, Is.EquivalentTo(answer.Values));
        }
        */
        [Test]
        public void GenerateOpenAnswerTest()
        {
            var testBuilder = new ObsoleteTestBuilder(testCards);
            testBuilder.GenerateTasks(3, typeof(OpenAnswerQuestion));
            var test = testBuilder.Build();
            
            Assert.That(test.Count, Is.EqualTo(3));
        }

        [Test]
        public void GenerateChoiceTest()
        {
            var testBuilder = new ObsoleteTestBuilder(testCards);
            testBuilder.GenerateTasks(3, typeof(ChoiceQuestion));
            var test = testBuilder.Build();
            
            Assert.That(test.Count, Is.EqualTo(3));
        }
    }
}
