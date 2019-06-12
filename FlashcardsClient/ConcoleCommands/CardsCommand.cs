
namespace FlashcardsClient
{
    public class CardsCommand : ConsoleCommand
    {
        public CardsCommand() : base("-acard", "Returns all cards")
        {
        }

        public override void Execute(FlashcardsClient client, string userName)
        {
            client.GetAllCards();
            UiHelpingMethods.GetCards(client);
        }
    }
}