using System;
using System.Collections.Generic;

using Flashcards;

namespace FlashcardsClient
{
    public static class UiHelpingMethods
    {
        public static List<Collection> GetCollections(FlashcardsClient client)
        {
            var collections = client.GetAllCollections() ?? client.LastReceivedCollections;
            Console.WriteLine("This is your collections");
            for (var i = 0; i < collections.Count; i++)
                Console.WriteLine($"{i}) {collections[i].Name}");
            return collections;
        }

        public static List<Card> GetCards(FlashcardsClient client)
        {
            var cards = client.GetAllCards() ?? client.LastReceivedCards;
            Console.WriteLine("This is your cards");
            for (var i = 0; i < cards.Count; i++)
                Console.WriteLine($"{i}) {cards[i].Term} — {cards[i].Definition}");
            return cards;
        }
    }
}