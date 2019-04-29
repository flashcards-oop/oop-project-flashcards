using Microsoft.AspNetCore.Authorization;

namespace FlashcardsApi
{
    public class SameOwnerRequirement : IAuthorizationRequirement
    { }

    public static class Policies
    {
        public const string ResourceAccess = "ResourceAccess";
    }
    
}
