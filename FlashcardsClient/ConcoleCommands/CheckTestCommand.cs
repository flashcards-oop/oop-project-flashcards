using System;
using System.Collections.Generic;
using FlashcardsClient.Infrastructure;

namespace FlashcardsClient
{
    public class CheckTestCommand : ConsoleCommand
    {
        public CheckTestCommand() : base("-check", "Gives a test to solving and check answers")
        {
        }

        private TestAnswers GetTestAnswers(FlashcardsClient client)
        {
            if (client.LastRecievedTest == null)
            {
                Console.WriteLine("You had not request for a test. Do it by command -test");
                return null;
            }
            var testAnswers = new List<ExerciseAnswer>();
            foreach (var exercise in client.LastRecievedTest.Exercises)
                testAnswers.Add(ExerciseHandler.HandleQuestion(exercise));
            return new TestAnswers { TestId = client.LastRecievedTest.TestId, Answers = testAnswers };
        }

        public override void Execute(FlashcardsClient client, string userName)
        {
            var testAnswers = GetTestAnswers(client);
            if (testAnswers == null)
                return;
            var chechedTest = client.GetCheckedTest(testAnswers);
            if (chechedTest.WrongAnswers == 0)
            {
                Console.WriteLine($"Correct answers: {chechedTest.CorrectAnswers}/{chechedTest.Answers.Count}");
                Console.WriteLine("Well done! You solved test correctly!");
            }
            else
            {
                Console.WriteLine($"Correct answers: {chechedTest.CorrectAnswers}/{chechedTest.Answers.Count}");
                Console.WriteLine($"Wrong answers: {chechedTest.WrongAnswers}/{chechedTest.Answers.Count}");
                Console.WriteLine("Correct answers for your mistakes");
                foreach (var answer in chechedTest.Answers)
                {
                    foreach (var question in client.LastRecievedTest.Exercises)
                    {
                        var id = question.Id;
                        if (answer.ContainsKey(id) && !answer[id].Correct)
                            ExerciseHandler.ShowCorrectAnswers(question, answer[id]);
                    }
                }
            }
        }
    }
}
