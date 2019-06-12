using System;
using System.Collections.Generic;

namespace FlashcardsClient
{
    public class ConsoleUi
    {
        private string userName;
        private FlashcardsClient flashcardsClient;
        private readonly List<ConsoleCommand> commands = new List<ConsoleCommand>();

        public ConsoleUi()
        {
            RegisterCommands();
            Console.WriteLine("Hello! Input your name please");
            ChangeUser();
        }

        private void RegisterCommands()
        {
            commands.Add(new AddCardCommand());
            commands.Add(new CardsCommand());
            commands.Add(new CheckTestCommand());
            commands.Add(new CollectionCardsCommand());
            commands.Add(new CollectionsCommand());
            commands.Add(new CreateCollectionCommand());
            commands.Add(new CreateTestCommand());
            commands.Add(new DeleteCardCommand());
            commands.Add(new DeleteCollectionCommand());
        }

        public void Help()
        {
            Console.WriteLine("Help for FlaschcardsClient");
            Console.WriteLine("-h Help");
            foreach (var command in commands)
                Console.WriteLine($"{command.Name} {command.Help}");
            Console.WriteLine("-off Turns off the application");
        }

        public void ChangeUser()
        {
            userName = Console.ReadLine();
            flashcardsClient = new FlashcardsClient(userName);
            Console.WriteLine($"Welcome, {userName}! Let's start! All commands you can find by -h");
        }

        public void Run()
        {
            var isRunning = true;
            while (isRunning)
            {
                var userCommand = Console.ReadLine();
                foreach (var command in commands)
                {
                    if (userCommand == "-off")
                    {
                        isRunning = false;
                        break;
                    }
                    if (userCommand == "-h")
                    {
                        Help();
                        break;
                    }
                    if (command.Name == userCommand)
                    {
                        command.Execute(flashcardsClient, userName);
                        break;
                    }
                    if (!commands.Contains(command))
                    {
                        Console.WriteLine("Unrecognized command. Please try again or use -h for help");
                    }
                }
            }
        }
    }
}