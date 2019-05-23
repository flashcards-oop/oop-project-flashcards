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
                new Card("t1", "d1", "0", "a"),
                new Card("t2", "d2", "0", "a")
            };
        
        private readonly List<Card> testCards = new List<Card>
        {
            new Card("1", "London", "is the capital of Great Britain", "a"),
            new Card("666", "Moscow", "is the capital of Russian Federation", "a"),
            new Card("13", "Mama", "Romama", "a")
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

        [Test]
        public void OpenQuestionExerciseGenerator_ShouldGenerateValidExercise()
        {
            var generator = new OpenQuestionExerciseGenerator();
            var card = new Card("term", "definition", "0", "a");
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
            Assert.That(question.Terms, Is.EquivalentTo(cards.Select(card => card.Term)));
            Assert.That(question.Definitions, Is.EquivalentTo(cards.Select(card => card.Definition)));
            //Assert.Contains("t1", question.Terms);
            //Assert.Contains("t2", question.Terms);
            //Assert.Contains("d1", question.Definitions);
            //Assert.Contains("d2", question.Definitions);

            var answer = exercise.Answer as MatchingAnswer;
            Assert.That(answer.Matches["d1"] == "t1");
            Assert.That(answer.Matches["d2"] == "t2");
        }

        [Test]
        public void ChoiceQuestionExerciseGenerator_ShouldGenerateValidExercise()
        {
            var generator = new ChoiceQuestionExerciseGenerator(2);
            var exercise = generator.GenerateExerciseFrom(cards);

            Assert.IsInstanceOf<ChoiceQuestion>(exercise.Question);
            Assert.IsInstanceOf<ChoiceAnswer>(exercise.Answer);

            var question = exercise.Question as ChoiceQuestion;
            Assert.Contains(question.Definition, cards.Select(card => card.Definition).ToList());
            Assert.That(question.Choices, Is.EquivalentTo(cards.Select(card => card.Term)));

            var answer = exercise.Answer as ChoiceAnswer;
            Assert.That(
                cards.Any(card => card.Definition == question.Definition && card.Term == answer.Answer));
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

            Assert.AreEqual(openCnt, exercises.Count(ex => ex.Question is OpenAnswerQuestion));
            Assert.AreEqual(matchingCnt, exercises.Count(ex => ex.Question is MatchingQuestion));
        }
    }
}
