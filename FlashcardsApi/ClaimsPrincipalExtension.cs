using Flashcards;
using System.Security.Claims;

namespace FlashcardsApi
{
    public static class ClaimsPrincipalExtension
    {
        public static bool OwnsResource(this ClaimsPrincipal user, IOwnedResource resource)
        {
            return user.Identity.Name == resource.OwnerLogin;
        }
    }
}
