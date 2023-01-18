using System.Security.Principal;

namespace Infrastructure.Services
{
    public interface IIdentityService<T>
    {
        T Get(IPrincipal principal);
    }
}
