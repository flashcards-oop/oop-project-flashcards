using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Flashcards;


namespace FlashcardsTests
{
    [TestFixture]
    public class TestGenerationTests
    {
        List<Card> cards = new List<Card>
            {
                new Card("t1", "d1", "0"),
                new Card("t2", "d2", "0")
            };

        [Test]
        public void OpenQuestionExerciseGenerator_ShouldGenerateValidExercise()
        {
            var generator = new OpenQuestionExerciseGenerator();
            var card = new Card("term", "definition", "0");
            var exercise = generator.GenerateExerciseFrom(new List<Card> { card });

            Assert.IsInstanceOf<OpenAnswer>(exercise.Answer);
            Assert.IsInstanceOf<OpenAnswerQuestion>(exercise.Question);
            Assert.AreEqual((exercise.Answer as OpenAnswer).Answer, card.Term);
            Assert.AreEqual((exercise.Question as OpenAnswerQuestion).Definition, card.Definition);
        }

        [Test]
        public void MatchingQuestionExerciseGenerator_ShouldGenerateValidExercise()
        {
            var generator = new MatchingQuestionExerciseGenerator(2);
            var exercise = generator.GenerateExerciseFrom(cards);

            Assert.IsInstanceOf<MatchingQuestion>(exercise.Question);
            Assert.IsInstanceOf<MatchingAnswer>(exercise.Answer);

            var question = exercise.Question as MatchingQuestion;
            Assert.Contains("t1", question.Terms);
            Assert.Contains("t2", question.Terms);
            Assert.Contains("d1", question.Definitions);
            Assert.Contains("d2", question.Definitions);

            var answer = exercise.Answer as MatchingAnswer;
            Assert.That(answer.Matches["d1"] == "t1");
            Assert.That(answer.Matches["d2"] == "t2");
        }

        [Test]
        public void TestBuilder_ShouldGenerateAllRequestedQuestions()
        {
            var builder = new TestBuilder(cards, new RandomCardsSelector());
            var openCnt = 3;
            var matchingCnt = 2;
            var exercises = builder
                .WithGenerator(new OpenQuestionExerciseGenerator(), openCnt)
                .WithGenerator(new MatchingQuestionExerciseGenerator(2), matchingCnt)
                .Build();

            Assert.AreEqual(openCnt, exercises.Where(ex => ex.Question is OpenAnswerQuestion).Count());
            Assert.AreEqual(matchingCnt, exercises.Where(ex => ex.Question is MatchingQuestion).Count());
        }
    }
}
