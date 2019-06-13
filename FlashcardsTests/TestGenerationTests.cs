using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Flashcards;
using Flashcards.Answers;
using Flashcards.Questions;
using Flashcards.TestProcessing;
using Flashcards.TestProcessing.QuestionGenerators;


namespace FlashcardsTests
{
    [TestFixture]
    public class TestGenerationTests
    {
        private static readonly Guid Id = Guid.NewGuid();
        
        private readonly List<Card> cards = new List<Card>
            {
                new Card("t1", "d1", "0", Id),
                new Card("t2", "d2", "0", Id)
            };
        
        private readonly List<Card> testCards = new List<Card>
        {
            new Card("1", "London", "is the capital of Great Britain", Id),
            new Card("666", "Moscow", "is the capital of Russian Federation", Id),
            new Card("13", "Mama", "Romama", Id)
        };
        
        [Test]
        public void GenerateOpenAnswerTest()
        {
            var exercises = new TestBuilder(testCards, new RandomCardsSelector())
                .WithGenerator(new OpenQuestionExerciseGenerator(), 3)
                .Build();
            
            Assert.That(exercises.Count(), Is.EqualTo(3));
            Assert.That(exercises.Select(ex => ex.Question), 
                Has.All.InstanceOf<OpenAnswerQuestion>());
        }

        [Test]
        public void GenerateChoiceTest()
        {
            var exercises = new TestBuilder(testCards, new RandomCardsSelector())
                .WithGenerator(new ChoiceQuestionExerciseGenerator(), 3)
                .Build();
            
            Assert.That(exercises.Count(), Is.EqualTo(3));
            Assert.That(exercises.Select(ex => ex.Question),
                Has.All.InstanceOf<ChoiceQuestion>());
        }

        [Test]
        public void GenerateMatchingTest()
        {
            var exercises = new TestBuilder(testCards, new RandomCardsSelector())
                .WithGenerator(new MatchingQuestionExerciseGenerator(), 3)
                .Build();
            
            Assert.That(exercises.Count(), Is.EqualTo(3));
            Assert.That(exercises.Select(ex => ex.Question),
                Has.All.InstanceOf<MatchingQuestion>());
        }

        [Test]
        public void OpenQuestionExerciseGenerator_ShouldGenerateValidExercise()
        {
            var generator = new OpenQuestionExerciseGenerator();
            var card = new Card("term", "definition", "0", Id);
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
           
            var answer = exercise.Answer as MatchingAnswer;
            Assert.AreEqual(answer.Matches["d1"], "t1");
            Assert.AreEqual(answer.Matches["d2"], "t2");
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
