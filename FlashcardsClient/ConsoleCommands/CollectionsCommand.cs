namespace FlashcardsClient.ConsoleCommands
{
    public class CollectionsCommand : ConsoleCommand
    {
        public CollectionsCommand() : base("-acol", "Returns all collections")
        {
        }

        public override void Execute(FlashcardsClient client, string userName)
        {
            UiHelpingMethods.GetCollections(client);
        }
    }
}
