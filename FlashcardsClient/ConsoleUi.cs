using System;
using System.Collections.Generic;
using Flashcards;
using FlashcardsClient.Infrastructure;

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
            var collections = flashcardsClient.GetAllCollections() ?? flashcardsClient.LastReceivedCollections;
            Console.WriteLine("This is your collections");
            for (var i = 0; i < collections.Count; i++)
                Console.WriteLine($"{i}) {collections[i].Name}");
            return collections;
        }

        public List<Card> GetCards()
        {
            var cards = flashcardsClient.GetAllCards() ?? flashcardsClient.LastReceivedCards;
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

        public void GenerateTest()
        {
            var collections = GetCollections();
            if (collections.Count != 0)
            {
                Console.WriteLine("From which collection do you want create a test?");
                Console.WriteLine("Enter the number of collection");
                var collectionNumber = int.Parse(Console.ReadLine());
                var cardsCount = flashcardsClient.GetCollectionCards(collectionNumber).Count;
                if (cardsCount != 0)
                {
                    Console.WriteLine(
                        "Here you can choose how much questioons of each type you need. You also can choose 0 if you don't want one of types");
                    Console.WriteLine("Enter the number of choice question");
                    var choiceNumber = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the number of matching question");
                    var matchingNumber = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the number of open answer question");
                    var openNumber = int.Parse(Console.ReadLine());
                    var request = new List<QuestionRequest>();
                    if (choiceNumber > 0)
                        request.Add(new QuestionRequest { Type = "choice", Amount = choiceNumber });
                    if (matchingNumber > 0)
                        request.Add(new QuestionRequest { Type = "matching", Amount = matchingNumber });
                    if (openNumber > 0)
                        request.Add(new QuestionRequest { Type = "open", Amount = openNumber });
                    flashcardsClient.GenerateTest(collectionNumber, request);
                }
                else
                {
                    Console.WriteLine("Whoops! You don't have any cards in this collection. How about adding new one by command -ac");
                }
            }
            else
            {
                Console.WriteLine("Whoops! You don't have any collections. How about creating new one by command -cc");
            }
        }

        public TestAnswers GetTestAnswers()
        {
            if (flashcardsClient.LastRecievedTest == null)
            {
                Console.WriteLine("You had not request for a test. Do it by command -test");
                return null;
            }
            var testAnswers = new List<ExerciseAnswer>();
            foreach (var exercise in flashcardsClient.LastRecievedTest.Exercises)
                testAnswers.Add(ExerciseHandler.HandleQuestion(exercise));
            return new TestAnswers { TestId = flashcardsClient.LastRecievedTest.TestId, Answers = testAnswers };
        }

        public void CheckTestAnswers()
        {
            var testAnswers = GetTestAnswers();
            if (testAnswers == null)
                return;
            var chechedTest = flashcardsClient.GetCheckedTest(testAnswers);
            if (chechedTest.WrongAnswersCount == 0)
            {
                Console.WriteLine($"Correct answers: {chechedTest.CorrectAnswersCount}/{chechedTest.CheckedAnswers.Count}");
                Console.WriteLine("Well done! You solved test correctly!");
            }
            else
            {
                Console.WriteLine($"Correct answers: {chechedTest.CorrectAnswersCount}/{chechedTest.CheckedAnswers.Count}");
                Console.WriteLine($"Wrong answers: {chechedTest.WrongAnswersCount}/{chechedTest.CheckedAnswers.Count}");

            }
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
                    case "-test":
                        GenerateTest();
                        break;
                    case "-check":
                        CheckTestAnswers();
                        break;
                    default:
                        Console.WriteLine("Unrecognized command. Please try again or use -h for help");
                        break;
                }
            }
        }
    }
}