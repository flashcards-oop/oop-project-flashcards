using System;

namespace FlashcardsClient.ConsoleCommands
{
    public class DeleteCollectionCommand : ConsoleCommand
    {
        public DeleteCollectionCommand() : base("-dcol", "Deletes collection")
        {
        }

        public override void Execute(FlashcardsClient client, string userName)
        {
            UiHelpingMethods.GetCollections(client);
            Console.WriteLine("Enter the number of collection");
            var number = int.Parse(Console.ReadLine());
            client.DeleteCollection(number);
            Console.WriteLine("Collection deleted!");
        }
    }
}