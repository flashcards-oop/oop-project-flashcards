using System;
using System.Collections.Generic;
using Flashcards;

namespace FlashcardsClient
{
    public class ConsoleUi
    {
        private string userName;
        private FlashcardsClient flashcardsClient;

        public ConsoleUi()
        {
            Console.WriteLine("Hello! Input your name please");
            ChangeUser();
        }

        public void Help()
        {
            Console.WriteLine("Help for FlaschcardsClient");
            Console.WriteLine("-h Help");
            Console.WriteLine("-cu Changes user");
            Console.WriteLine("-cc Creates new collection");
            Console.WriteLine("-ac Adds card to collection");
            Console.WriteLine("-acc Returns all cards in collection");
            Console.WriteLine("-acol Returns all collections");
            Console.WriteLine("-acard Returns all cards");
            Console.WriteLine("-dcol Deletes collection");
            Console.WriteLine("-dcard Deletes card");
            Console.WriteLine("-off Turns off the application");
        }

        public void ChangeUser()
        {
            userName = Console.ReadLine();
            flashcardsClient = new FlashcardsClient(userName);
            Console.WriteLine($"Welcome, {userName}! Let's start! All commands you can find by -h");
        }

        public void CreateNewCollection()
        {
            Console.WriteLine("Input name for new collection");
            var collectionName = Console.ReadLine();
            flashcardsClient.CreateCollection(collectionName);
        }

        public List<Collection> GetCollections()
        {
            var collections = flashcardsClient.LastReceivedCollections == null
                ? flashcardsClient.GetAllCollections()
                : flashcardsClient.LastReceivedCollections;
            Console.WriteLine("This is your collections");
            for (var i = 0; i < collections.Count; i++)
                Console.WriteLine($"{i}) {collections[i].Name}");
            return collections;
        }

        public List<Card> GetCards()
        {
            var cards = flashcardsClient.LastReceivedCards == null
                ? flashcardsClient.GetAllCards()
                : flashcardsClient.LastReceivedCards;
            Console.WriteLine("This is your cards");
            for (var i = 0; i < cards.Count; i++)
                Console.WriteLine($"{i}) {cards[i].Term} — {cards[i].Definition}");
            return cards;
        }

        public void AddCardToCollection()
        {
            var collections = GetCollections();
            Console.WriteLine("Enter the number of collection");
            var number = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the term");
            var term = Console.ReadLine();
            Console.WriteLine("Enter the definition");
            var definition = Console.ReadLine();
            var card = new Card(term, definition, userName, collections[number].Id);
            flashcardsClient.AddCardToCollection(card);
        }

        public void ShowCollectionCards()
        {
            GetCollections();
            Console.WriteLine("Enter the number of collection");
            var number = int.Parse(Console.ReadLine());
            flashcardsClient.GetCollectionCards(number);
            GetCards();
        }

        public void DeleteCollection()
        {
            GetCollections();
            Console.WriteLine("Enter the number of collection");
            var number = int.Parse(Console.ReadLine());
            flashcardsClient.DeleteCollection(number);
        }

        public void DeleteCard()
        {
            GetCards();
            Console.WriteLine("Enter the number of card");
            var number = int.Parse(Console.ReadLine());
            flashcardsClient.DeleteCard(number);
        }

        public void Run()
        {
            var isRunning = true;
            while (isRunning)
            {
                var command = Console.ReadLine();
                switch (command)
                {
                    case "-h":
                        Help();
                        break;
                    case "-cu":
                        Console.WriteLine("Input new userName");
                        ChangeUser();
                        break;
                    case "-cc":
                        CreateNewCollection();
                        break;
                    case "-ac":
                        AddCardToCollection();
                        break;
                    case "-acc":
                        ShowCollectionCards();
                        break;
                    case "-acol":
                        GetCollections();
                        break;
                    case "-acard":
                        flashcardsClient.GetAllCards();
                        GetCards();
                        break;
                    case "-dcol":
                        DeleteCollection();
                        break;
                    case "-dcard":
                        DeleteCard();
                        break;
                    case "-off":
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Unrecognized command. Please try again or use -h for help");
                        break;
                }
            }
        }
    }
}