using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RestSharp;
using Flashcards;

namespace FlashcardsClient
{
    public class FlashcardsClient
    {
        private readonly RestClient client;
        private readonly string token;
        public List<Card> LastReceivedCards;
        public List<Collection> LastReceivedCollections;

        public void AddUser(string userName)
        {
            var request = new RestRequest("api/users/create");
            request.AddJsonBody(userName);
            var response = client.Post(request);
            Console.WriteLine(response.Content);
        }

        private string GetToken(string userName)
        {
            var request = new RestRequest("api/users/token");
            request.AddJsonBody(userName);
            var response = client.Post(request).Content;
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(response)["access_token"];
        }

        public FlashcardsClient(string name)
        {
            client = new RestClient("http://localhost:5000");
            AddUser(name);
            token = GetToken(name);
            LastReceivedCards = null;
            LastReceivedCollections = null;
        }

        public void CreateCollection(string collectionName)
        {
            var request = new RestRequest("api/collections/create");
            request.AddJsonBody(collectionName);
            request.AddHeader("Authorization", $"Bearer {token}");
            var response = client.Post(request);
            Console.WriteLine(response.Content);
        }

        public List<Collection> GetAllCollections()
        {
            var request = new RestRequest("api/collections/all");
            request.AddHeader("Authorization", $"Bearer {token}");
            var response = client.Get(request).Content;
            var parsedResponse = JsonConvert.DeserializeObject<List<Collection>>(response);
            LastReceivedCollections = new List<Collection>();
            LastReceivedCollections.AddRange(parsedResponse);
            return parsedResponse;
        }

        public Collection GetCollectionById(string id)
        {
            var request = new RestRequest($"api/collections/{id}");
            request.AddHeader("Authorization", $"Bearer {token}");
            var response = client.Get(request).Content;
            var parsedResponse = JsonConvert.DeserializeObject<Collection>(response);
            return parsedResponse;
        }

        public void AddCardToCollection(Card card)
        {
            var request = new RestRequest("api/cards/create");
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddHeader("Content-Type", "application/json");
            var body = new Dictionary<string, string>
            {
                ["collectionId"] = card.CollectionId,
                ["term"] = card.Term,
                ["definition"] = card.Definition
            };
            request.AddJsonBody(body);
            var response = client.Post(request).Content;
            Console.WriteLine(response);
        }

        public void DeleteCollection(int number)
        {
            var id = LastReceivedCollections[number].Id;
            var cards = GetCollectionCards(number);
            for (var i = 0; i < cards.Count; i++)
            {
                DeleteCard(i);
            }
            var request = new RestRequest("api/collections/delete");
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(id);
            var response = client.Delete(request).Content;
            Console.WriteLine(response);
            GetAllCollections();
        }

        public List<Card> GetAllCards()
        {
            var request = new RestRequest("api/cards/all");
            request.AddHeader("Authorization", $"Bearer {token}");
            var response = client.Get(request).Content;
            var parsedResponse = JsonConvert.DeserializeObject<List<Card>>(response);
            LastReceivedCards = new List<Card>();
            LastReceivedCards.AddRange(parsedResponse);
            return parsedResponse;
        }

        public List<Card> GetCollectionCards(int number)
        {
            var id = LastReceivedCollections[number].Id;
            var request = new RestRequest($"api/collections/{id}/cards");
            request.AddHeader("Authorization", $"Bearer {token}");
            var response = client.Get(request).Content;
            var parsedResponse = JsonConvert.DeserializeObject<List<Card>>(response);
            LastReceivedCards = new List<Card>();
            LastReceivedCards.AddRange(parsedResponse);
            return parsedResponse;
        }

        public Card GetCardById(string id)
        {
            var request = new RestRequest($"api/cards/{id}");
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddHeader("Content-Type", "application/json");
            var response = client.Get(request).Content;
            var parsedResponse = JsonConvert.DeserializeObject<Card>(response);
            return parsedResponse;
        }

        public void DeleteCard(int number)
        {
            var id = LastReceivedCards[number].Id;
            var request = new RestRequest("api/cards/delete");
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddJsonBody(id);
            var response = client.Delete(request).Content;
            Console.WriteLine(response);
            GetAllCards();
        }
    }
}