using System;

namespace FlashcardsClient
{
    public class CreateCollectionCommand : ConsoleCommand
    {
        public CreateCollectionCommand() : base("-cc", "Create new collection")
        {
        }

        public override void Execute(FlashcardsClient client, string userName)
        {
            Console.WriteLine("Input name for new collection");
            var collectionName = Console.ReadLine();
            client.CreateCollection(collectionName);
            Console.WriteLine("Collection created!");
        }
    }
}
