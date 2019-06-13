using System;
using System.Collections.Generic;
using System.Linq;

namespace FlashcardsClient.ConsoleCommands
{
    public class CheckTestCommand : ConsoleCommand
    {
        public CheckTestCommand() : base("-check", "Gives a test to solving and check answers")
        {
        }

        private static TestAnswers GetTestAnswers(FlashcardsClient client)
        {
            if (client.LastReceivedTest == null)
            {
                Console.WriteLine("You had not request for a test. Do it by command -test");
                return null;
            }
            var testAnswers = new List<ExerciseAnswer>();
            foreach (var exercise in client.LastReceivedTest.Exercises)
                testAnswers.Add(ExerciseHandler.HandleQuestion(exercise));
            return new TestAnswers { TestId = client.LastReceivedTest.TestId, Answers = testAnswers };
        }

        public override void Execute(FlashcardsClient client, string userName)
        {
            var testAnswers = GetTestAnswers(client);
            if (testAnswers == null)
                return;
            var testVerdict = client.GetCheckedTest(testAnswers);
            if (testVerdict.WrongAnswers == 0)
            {
                Console.WriteLine($"Correct answers: {testVerdict.CorrectAnswers}/{testVerdict.Answers.Count}");
                Console.WriteLine("Well done! You solved test correctly!");
            }
            else
            {
                Console.WriteLine($"Correct answers: {testVerdict.CorrectAnswers}/{testVerdict.Answers.Count}");
                Console.WriteLine($"Wrong answers: {testVerdict.WrongAnswers}/{testVerdict.Answers.Count}");
                Console.WriteLine("Correct answers for your mistakes");
                foreach (var (id, verdict) in testVerdict.Answers)
                {
                    if (!verdict.Correct)
                        ExerciseHandler.ShowCorrectAnswers(
                            client.LastReceivedTest.Exercises.First(e => e.Id == id).Question, 
                            verdict.CorrectAnswer);
                }
            }
        }
    }
}
