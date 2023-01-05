using Security.Application.Queries.ViewModels;
using Security.Application.Wrappers;

namespace Security.Application.Queries.Interfaces
{
    public interface IUserAuthenticationQuery
    {
        Task<IEnumerable<UserAuthenticationViewModel>> Search(UserAuthenticationRequest userAuthenticationRequest);
        Task<Response<UserAuthenticationViewModel>> AuthenticationUserToken(UserAuthenticationRequest userAuthenticationRequest);
    }
}
