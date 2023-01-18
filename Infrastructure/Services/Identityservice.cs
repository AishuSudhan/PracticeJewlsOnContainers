using Infrastructure.Models;
using System.Security.Claims;
using System.Security.Principal;

namespace Infrastructure.Services
{
    public class Identityservice : IIdentityService<ApplicationUser>
    {
        public ApplicationUser Get(IPrincipal principal)//principal is the identity of the person.
            //by the time line 9 hits the Token is being issued already.
        {
            
            if (principal is ClaimsPrincipal claims)//checks is it a Token that contains claims?
            {
                var user = new ApplicationUser
                {
                    Email = claims.Claims.FirstOrDefault(x => x.Type == "preferred_username")?.Value ?? "",
                    Id = claims.Claims.FirstOrDefault(x => x.Type == "preferred_username")?.Value ?? "",
                    //this is how the token looks like.
                };
                return user;
            }
            //if token doesn't exists it will thow the exception message.
            throw new ArgumentException(message: "The principal must be a claimsprincipal",
                paramName: nameof(principal));
        }
    }
}
