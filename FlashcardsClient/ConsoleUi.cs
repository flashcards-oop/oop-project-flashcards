using System;
using System.Collections.Generic;
using FlashcardsClient.ConsoleCommands;

namespace FlashcardsClient
{
    public class ConsoleUi
    {
        private string userName;
        private FlashcardsClient flashcardsClient;
        private readonly List<ConsoleCommand> commands;

        public ConsoleUi(List<ConsoleCommand> commands)
        {
            this.commands = commands;
            Console.WriteLine("Hello! Input your name please");
            ChangeUser();
        }

        private void Help()
        {
            Console.WriteLine("Help for FlashcardsClient");
            Console.WriteLine("-h Help");
            foreach (var command in commands)
                Console.WriteLine($"{command.Name} {command.Help}");
            Console.WriteLine("-off Turns off the application");
        }

        private void ChangeUser()
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
                    if (command.Name.Equals(userCommand))
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