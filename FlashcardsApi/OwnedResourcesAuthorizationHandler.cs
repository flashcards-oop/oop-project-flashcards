using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Flashcards;

namespace FlashcardsApi
{
    public class OwnedResourcesAuthorizationHandler : AuthorizationHandler<SameOwnerRequirement, IOwnedResource>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            SameOwnerRequirement requirement, 
            IOwnedResource resource)
        {
            if (context.User.Identity.Name == resource.OwnerLogin)
                context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }

    public class SameOwnerRequirement : IAuthorizationRequirement
    { }
}
