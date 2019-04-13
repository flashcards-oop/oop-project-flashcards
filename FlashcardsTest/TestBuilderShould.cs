using System;
using NUnit.Framework;
using Flashcards;
using System.Collections.Generic;
using System.Linq;

namespace FlashcardsTest
{
    [TestFixture]
    public class TestBuilderShould
    {
        private readonly List<Card> testCards = new List<Card>
        {
            new Card(1, "London", "is the capital of Great Britain"),
            new Card(666, "Moscow", "is the capital of Russian Federation"),
            new Card(13, "Mama", "Romama")
        };

        [Test]
        public void GenerateMatchingTest()
        {
            var testBuilder = new TestBuilder(testCards);
            testBuilder.GenerateTasks(1, typeof(MatchingQuestion));
            var test = testBuilder.Build();
            var answer = new Dictionary<string, string>
            {
                ["London"] = "is the capital of Great Britain",
                ["Moscow"] = "is the capital of Russian Federation",
                ["Mama"] = "Romama"
            };
            var exercise = new Exercise(new MatchingAnswer(answer),
                new MatchingQuestion(answer.Keys.ToArray(), answer.Values.ToArray()));

            //Assert.Contains(exercise, test);
        }

        [Test]
        public void GenerateOpenAnswerTest()
        {
            var testBuilder = new TestBuilder(testCards);
            testBuilder.GenerateTasks(3, typeof(OpenAnswerQuestion));
            var test = testBuilder.Build();
            var exercise = new Exercise(new OpenAnswer("London"),
                new OpenAnswerQuestion("is the capital of Great Britain"));

            Assert.Contains(exercise, test);
        }

        [Test]
        public void GenerateChoiceTest()
        {
            var testBuilder = new TestBuilder(testCards);
            testBuilder.GenerateTasks(3, typeof(ChoiceQuestion));
            var test = testBuilder.Build();
            var exercise = new Exercise(new ChoiceAnswer("Moscow"),
                new ChoiceQuestion("is the capital of Russian Federation",
                    new[] { "is the capital of Great Britain", "is the capital of Russian Federation", "Romama" }));

            //Assert.Contains(exercise, test);
        }
    }
}
