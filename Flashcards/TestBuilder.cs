using System;
using System.Collections.Generic;
using System.Linq;

namespace Flashcards
{
    public class TestBuilder
    {
        private readonly List<Exercise> exercises;
        private readonly List<Card> cards;
        private Random Random { get; }
        private readonly Dictionary<Type, Func<Exercise>> exerciseGenerators;
        private int choicesNumber = 4;
        private int matchingNumber = 3;

        public TestBuilder(List<Card> cards)
        {
            this.cards = cards;
            exercises = new List<Exercise>();
            Random = new Random();
            exerciseGenerators = new Dictionary<Type, Func<Exercise>>
            {
                [typeof(ChoiceQuestion)] = GenerateChoicesTasks,
                [typeof(MatchingQuestion)] = GenerateMatchingTasks,
                [typeof(OpenAnswerQuestion)] = GenerateSimpleTasks
            };
        }

        public List<Exercise> Build()
        {
            return exercises;
        }

        public void GenerateTasks(int exerciseNumber, Type questionsType)
        {
            if (exerciseNumber > cards.Count)
                exerciseNumber = cards.Count;
            var exerciseCounter = 0;
            while (exerciseCounter < exerciseNumber)
            {
                var task = exerciseGenerators[questionsType]();
                if (exercises.Contains(task))
                    continue;
                exercises.Add(task);
                exerciseCounter++;
            }
        }

        private Exercise GenerateMatchingTasks()
        {
            var answer = new Dictionary<string, string>();
            var matchingCounter = 0;
            while (matchingCounter < matchingNumber)
            {
                var index = Random.Next(cards.Count);
                if (answer.ContainsKey(cards[index].Term))
                    continue;
                answer[cards[index].Term] = cards[index].Definition;
                matchingCounter++;
            }
            var terms = answer.Keys.ToArray();
            terms.Shuffle();
            var definitions = answer.Values.ToArray();
            definitions.Shuffle();
            return new Exercise(new MatchingAnswer(answer), new MatchingQuestion(terms, definitions));
        }

        private Exercise GenerateChoicesTasks()
        {
            var index = Random.Next(cards.Count);
            var choices = new string[choicesNumber];
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
            return new Exercise(new ChoiceAnswer(answer), new ChoiceQuestion(definition, choices));
        }

        private Exercise GenerateSimpleTasks()
        {
            var index = Random.Next(cards.Count);
            return new Exercise(new OpenAnswer(cards[index].Term), new OpenAnswerQuestion(cards[index].Definition));
        }
    }
}