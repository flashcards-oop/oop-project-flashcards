using RestSharp;

namespace FlashcardsClient
{
    public static class Extensions
    {
        public static void AddAuthorization(this RestRequest request, string token)
        {
            request.AddHeader("Authorization", $"Bearer {token}");
        }

        public static void AddJsonApp(this RestRequest request)
        {
            request.AddHeader("Content-Type", "application/json");
        }
    }
}