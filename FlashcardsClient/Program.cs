using System;
using System.Net.Http;
using RestSharp;

namespace FlashcardsClient
{
    class Program
    {
        static void AddUser(string userName)
        {
            var client = new RestClient("http://localhost:17720");
            var request = new RestRequest("api/users/create");
            request.AddJsonBody(userName);

            var response = client.Post(request);
            Console.WriteLine(response.StatusCode);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("start");
            AddUser("Vika");
        }
    }
}
