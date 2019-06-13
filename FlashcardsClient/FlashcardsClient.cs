using System.Collections.Generic;
using Newtonsoft.Json;
using RestSharp;
using Flashcards;
using FlashcardsClient.Infrastructure;


namespace FlashcardsClient
{
    public class FlashcardsClient
    {
        private readonly RestClient client;
        private readonly string token;
        public List<Card> LastReceivedCards;
        public List<Collection> LastReceivedCollections;
        public Test LastReceivedTest;

        private static readonly JsonSerializerSettings SerializerSettings = 
            new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.Auto};

        public FlashcardsClient(string name)
        {
            client = new RestClient("http://localhost:5000");
            AddUser(name);
            token = GetToken(name);
            LastReceivedCards = null;
            LastReceivedCollections = null;
            LastReceivedTest = null;
        }

        private void AddUser(string userName)
        {
            var request = new RestRequest("api/users/create");
            request.AddJsonBody(userName);
            client.Post(request);
        }

        private string GetToken(string userName)
        {
            var request = new RestRequest("api/users/token");
            request.AddJsonBody(userName);
            var response = client.Post(request).Content;
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(response)["access_token"];
        }

        public void CreateCollection(string collectionName)
        {
            var request = new RestRequest("api/collections/create");
            request.AddJsonBody(collectionName);
            request.AddAuthorization(token);
            client.Post(request);
        }

        public List<Collection> GetAllCollections()
        {
            var request = new RestRequest("api/collections/all");
            request.AddAuthorization(token);
            var response = client.Get(request).Content;
            var parsedResponse = JsonConvert.DeserializeObject<List<Collection>>(response);
            LastReceivedCollections = new List<Collection>();
            LastReceivedCollections.AddRange(parsedResponse);
            return parsedResponse;
        }

        public void AddCardToCollection(Card card)
        {
            var request = new RestRequest("api/cards/create");
            request.AddAuthorization(token);
            request.AddJsonApp();
            request.AddJsonBody(new CardModel
            {
                CollectionId = card.CollectionId, 
                Term = card.Term, 
                Definition = card.Definition
            });
            client.Post(request);
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
            request.AddAuthorization(token);
            request.AddJsonApp();
            request.AddJsonBody(id);
            client.Delete(request);
            GetAllCollections();
        }

        public List<Card> GetAllCards()
        {
            var request = new RestRequest("api/cards/all");
            request.AddAuthorization(token);
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
            request.AddAuthorization(token);
            var response = client.Get(request).Content;
            var parsedResponse = JsonConvert.DeserializeObject<List<Card>>(response);
            LastReceivedCards = new List<Card>();
            LastReceivedCards.AddRange(parsedResponse);
            return parsedResponse;
        }

        public void DeleteCard(int number)
        {
            var id = LastReceivedCards[number].Id;
            var request = new RestRequest("api/cards/delete");
            request.AddAuthorization(token);
            request.AddJsonBody(id);
            client.Delete(request);
            GetAllCards();
        }

        public void GenerateTest(int number, List<TestBlock> userRequests)
        {
            var id = LastReceivedCollections[number].Id;
            var request = new RestRequest("api/tests/generate");
            request.AddAuthorization(token);
            request.AddJsonApp();
            var body = new TestRequest
            {
                CollectionId = id,
                Blocks = userRequests
            };
            request.AddJsonBody(body);
            var response = client.Post(request).Content;
            var parsedResponse = JsonConvert.DeserializeObject<Test>(response, SerializerSettings);
            LastReceivedTest = parsedResponse;
        }

        public TestVerdict GetCheckedTest(TestAnswers solution)
        {
            var request = new RestRequest("api/tests/check");
            request.AddAuthorization(token);
            request.AddJsonApp();
            request.AddJsonBody(JsonConvert.SerializeObject(solution, SerializerSettings));
            var response = client.Post(request).Content;
            var parsedResponse = JsonConvert.DeserializeObject<TestVerdict>(response, SerializerSettings);
            return parsedResponse;
        }
    }
}