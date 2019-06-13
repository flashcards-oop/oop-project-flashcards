using System;
using System.Collections.Generic;

namespace FlashcardsClient.ConsoleCommands
{
    public class CreateTestCommand : ConsoleCommand
    {
        private static void AddQuestionRequest(ICollection<TestBlock> request, string enteringText, string type)
        {
            Console.WriteLine(enteringText);
            var number = int.Parse(Console.ReadLine());
            if (number > 0)
                request.Add(new TestBlock { Type = type, Amount = number });
        }

        public CreateTestCommand() : base("-test", "Generates new test")
        {
        }

        public override void Execute(FlashcardsClient client, string userName)
        {
            var collections = UiHelpingMethods.GetCollections(client);
            if (collections.Count != 0)
            {
                Console.WriteLine("From which collection do you want create a test?");
                Console.WriteLine("Enter the number of collection");
                var collectionNumber = int.Parse(Console.ReadLine());
                var cardsCount = client.GetCollectionCards(collectionNumber).Count;
                if (cardsCount != 0)
                {
                    Console.WriteLine(
                        "Here you can choose how much questions of each type you need. You also can choose 0 if you don't want one of types");
                    var request = new List<TestBlock>();
                    AddQuestionRequest(request, "Enter the number of choice question", "choice");
                    AddQuestionRequest(request, "Enter the number of matching question", "matching");
                    AddQuestionRequest(request, "Enter the number of open answer question", "open");
                    client.GenerateTest(collectionNumber, request);
                    Console.WriteLine("Test created! You can pass it and see results by command -check");
                }
                else
                {
                    Console.WriteLine("Whoops! You don't have any cards in this collection. How about adding new one by command -ac");
                }
            }
            else
            {
                Console.WriteLine("Whoops! You don't have any collections. How about creating new one by command -cc");
            }
        }
    }
}