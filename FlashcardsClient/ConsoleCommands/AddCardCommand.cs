using System;
using Flashcards;

namespace FlashcardsClient.ConsoleCommands
{
    public class AddCardCommand : ConsoleCommand
    {
        public AddCardCommand() : base("-ac", "Adds card to collection")
        {
        }

        public override void Execute(FlashcardsClient client, string userName)
        {
            var collections = UiHelpingMethods.GetCollections(client);
            Console.WriteLine("Enter the number of collection");
            var number = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the term");
            var term = Console.ReadLine();
            Console.WriteLine("Enter the definition");
            var definition = Console.ReadLine();
            var card = new Card(term, definition, userName, collections[number].Id);
            client.AddCardToCollection(card);
            Console.WriteLine($"Card created and added to collection '{collections[number].Name}'");
        }
    }
}