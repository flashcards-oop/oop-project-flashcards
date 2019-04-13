using System;
using System.Collections.Generic;
using System.Linq;

namespace Flashcards
{
    public class TestBuilder
    {
        private readonly List<Task> test;
        private readonly List<Card> cards;
        private Random Random { get; }
        private readonly Dictionary<Type, Func<Task>> callinYourMother;

        public TestBuilder(List<Card> cards)
        {
            this.cards = cards;
            test = new List<Task>();
            Random = new Random();
            callinYourMother = new Dictionary<Type, Func<Task>>
            {
                [typeof(ChoiceQuestion)] = GenerateChoicesTasks,
                [typeof(MatchingQuestion)] = GenerateMatchingTasks,
                [typeof(SimpleQuestion)] = GenerateSimpleTasks
            };
        }

        public List<Task> Build()
        {
            return test;
        }

        public void GenerateTasks(int tasksNumber, Type questionsType)
        {
            if (tasksNumber > cards.Count)
                tasksNumber = cards.Count;
            var tasksCounter = 0;
            while (tasksCounter < tasksNumber)
            {
                var task = callinYourMother[questionsType]();
                if (test.Contains(task))
                    continue;
                test.Add(task);
                tasksCounter++;
            }
        }

        private Task GenerateMatchingTasks()
        {
            var answer = new Dictionary<string, string>();
            for (var i = 0; i < 5; i++)
            {
                var index = Random.Next(cards.Count);
                answer[cards[index].Term] = cards[index].Definition;
            }
            var terms = answer.Keys.ToArray();
            terms.Shuffle();
            var definitions = answer.Values.ToArray();
            definitions.Shuffle();
            return new Task(new MatchingAnswer(answer), new MatchingQuestion(terms, definitions));
        }

        private Task GenerateChoicesTasks()
        {
            var index = Random.Next(cards.Count);
            var choices = new string[4];
            var definition = cards[index].Definition;
            var answer = cards[index].Term;
            choices[0] = answer;
            for (var i = 1; i < choices.Length; i++)
            {
                index = Random.Next(cards.Count);
                var newChoice = cards[index].Term;
                if (choices.Contains(newChoice))
                    continue;
                choices[i] = newChoice;
            }
            choices.Shuffle();
            return new Task(new ChoiceAnswer(answer), new ChoiceQuestion(definition, choices));
        }

        private Task GenerateSimpleTasks()
        {
            var index = Random.Next(cards.Count);
            return new Task(new SimpleAnswer(cards[index].Term), new SimpleQuestion(cards[index].Definition));
        }
    }
}