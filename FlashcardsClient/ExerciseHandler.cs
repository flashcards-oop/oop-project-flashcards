using System;
using System.Collections.Generic;
using Flashcards;

namespace FlashcardsClient
{
    public static class ExerciseHandler
    {
        private static readonly Dictionary<Type, Func<ExerciseQuestion, ExerciseAnswer>> QuestionHandler =
            new Dictionary<Type, Func<ExerciseQuestion, ExerciseAnswer>>
            {
                {
                    typeof(OpenAnswerQuestion), e =>
                    {
                        Console.WriteLine(((OpenAnswerQuestion) e.Question).Definition);
                        Console.WriteLine("Enter the term matching to this definition");
                        
                        var answer = Console.ReadLine();
                        return new ExerciseAnswer{Id = e.Id, Answer = new OpenAnswer(answer)};
                    }
                },
                { 
                    typeof(ChoiceQuestion), e =>
                    {
                        var question = (ChoiceQuestion) e.Question;
                        Console.WriteLine(question.Definition);
                        Console.WriteLine("Choose the answer and enter the number of it");
                        for (var i = 0; i < question.Choices.Length; i++)
                            Console.WriteLine($"{i}) {question.Choices[i]}");
                        
                        var answer = int.Parse(Console.ReadLine());
                        return new ExerciseAnswer{Id = e.Id, Answer = new ChoiceAnswer(question.Choices[answer])};
                    }
                },
                {
                    typeof(MatchingQuestion), e =>
                    {
                        var question = (MatchingQuestion) e.Question;
                        Console.WriteLine("Here you can see terms and definitions in two columns. Match them");
                        for (var i = 0; i < question.Terms.Length; i++)
                            Console.WriteLine($"{i}) {question.Terms[i]}    {i}) {question.Definitions[i]}");
                        
                        var answer = new Dictionary<string, string>();
                        Console.WriteLine("And here you will be given the numbers of terms. Please, enter the number of definition for each one");
                        for (var i = 0; i < question.Terms.Length; i++)
                        {
                            Console.Write($"{i}) ");
                            var answerNumber = int.Parse(Console.ReadLine());
                            answer[question.Terms[i]] = question.Definitions[answerNumber];
                        }
                        return new ExerciseAnswer{Id = e.Id, Answer = new MatchingAnswer(answer)};
                    }
                }
            };

        private static readonly Dictionary<Type, Action<IQuestion, IAnswer>> CheckedAnswersHandler =
            new Dictionary<Type, Action<IQuestion, IAnswer>>
            {
                {
                    typeof(OpenAnswer), (e, c) =>
                    {
                        Console.WriteLine("Open answer question");
                        Console.WriteLine($"{((OpenAnswerQuestion) e).Definition} — {((OpenAnswer)c).Answer}");
                    }
                },
                {
                    typeof(ChoiceAnswer), (e, c) =>
                    {
                        Console.WriteLine("Choice question");
                        Console.WriteLine($"{((ChoiceQuestion) e).Definition} — {((ChoiceAnswer)c).Answer}");
                    }
                },
                {
                    typeof(MatchingAnswer), (e, c) =>
                    {
                        Console.WriteLine("Matching question");
                        var matches = ((MatchingAnswer) c).Matches;
                        foreach (var matching in matches.Keys)
                            Console.WriteLine($"{matching} — {matches[matching]}");
                    }
                }
            };

        public static ExerciseAnswer HandleQuestion(ExerciseQuestion question)
        {
            return QuestionHandler[question.Question.GetType()](question);
        }

        public static void ShowCorrectAnswers(IQuestion question, IAnswer answer)
        {
            CheckedAnswersHandler[answer.GetType()](question, answer);
        }
    }
}