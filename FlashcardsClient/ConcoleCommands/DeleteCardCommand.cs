using System;

namespace FlashcardsClient
{
    public class DeleteCardCommand : ConsoleCommand
    {
        public DeleteCardCommand() : base("-dcard", "Deletes card")
        {
        }

        public override void Execute(FlashcardsClient client, string userName)
        {
            UiHelpingMethods.GetCards(client);
            Console.WriteLine("Enter the number of card");
            var number = int.Parse(Console.ReadLine());
            client.DeleteCard(number);
            Console.WriteLine("Card deleted!");
        }
    }
}