using System.Security.Principal;

namespace Infrastructure.Services
{
    public interface IIdentityService<T>
    {
        T Get(IPrincipal principal);//principal is user
    }
}
//we are declaring IIdentity Service is Generic here(IIdentityService<T>) because depending on the type of token you get back.
//Token service is not only for getting token. infuture we may ask a tokenservice to do some other thing thing too.
//when a user comes to my website when they are trying to go somewhere which requires a Token,T Get(IPrincipal principal); this method
//will be fired which will pass the user information(principal) to say this user needs a Token,get me that token back.
//Code for that is in identity service class.