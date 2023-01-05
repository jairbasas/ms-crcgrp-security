using Security.Application.Queries.ViewModels;

namespace Security.Application.Queries.Interfaces
{
    public interface ITokenQuery
    {
        Task<IEnumerable<TokenViewModel>> GenerateToken(TokenRequest tokenRequest);
        Task<string> GenerateRefreshToken();
        Task<IEnumerable<TokenViewModel>> RefreshToken(TokenRequest tokenRequest);
    }
}
