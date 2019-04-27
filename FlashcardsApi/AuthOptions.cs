using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace FlashcardsApi
{
    public class AuthOptions
    {
        public const string Issuer = "FlashcardsAPI";
        public const string Audience = "";
        const string Key = "not_a_secret_at_all";
        public const int Lifetime = 1;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}
