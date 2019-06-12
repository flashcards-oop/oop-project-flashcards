using System;

namespace FlashcardsClient
{
    public class CollectionCardsCommand : ConsoleCommand
    {
        public CollectionCardsCommand() : base("-acc", "Returns all cards in collection")
        {
        }

        public override void Execute(FlashcardsClient client, string userName)
        {
            UiHelpingMethods.GetCollections(client);
            Console.WriteLine("Enter the number of collection");
            var number = int.Parse(Console.ReadLine());
            client.GetCollectionCards(number);
            UiHelpingMethods.GetCards(client);
        }
    }
}
