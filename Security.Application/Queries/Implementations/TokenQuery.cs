using Microsoft.IdentityModel.Tokens;
using Security.Application.Queries.Interfaces;
using Security.Application.Queries.ViewModels;
using Security.Application.Utility;
using Security.Domain.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace Security.Application.Queries.Implementations
{
    public class TokenQuery : ITokenQuery
    {
        private readonly string _key;
        private readonly int _durationInMinute;
        private readonly string _issuer;
        private readonly string _audience;

        public TokenQuery(string key, int durationInMinute, string issuer, string audience)
        {
            _key = key;
            _durationInMinute = durationInMinute;
            _issuer = issuer;
            _audience = audience;
        }

        public async Task<string> GenerateRefreshToken()
        {
            var ramdonNumber = new byte[32];
            using (var ramdonNumberGenerator = RandomNumberGenerator.Create())
            {
                ramdonNumberGenerator.GetBytes(ramdonNumber);
                return await Task.FromResult(Convert.ToBase64String(ramdonNumber));
            }
        }

        public async Task<IEnumerable<TokenViewModel>> GenerateToken(TokenRequest tokenRequest)
        {
            var claims = new[]
            {
                new System.Security.Claims.Claim(JwtRegisteredClaimNames.Sub, tokenRequest.userName),
                new System.Security.Claims.Claim(JwtRegisteredClaimNames.Email, tokenRequest.email),
                new System.Security.Claims.Claim("uid", tokenRequest.userId.ToString()),
                new System.Security.Claims.Claim("profile", tokenRequest.profileId.ToString()),
                new System.Security.Claims.Claim("company", tokenRequest.companyId.ToString())
            };

            var keySecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._key));
            var creds = new SigningCredentials(keySecret, SecurityAlgorithms.HmacSha256);
            var durationInMinute = DateTime.UtcNow.AddMinutes(this._durationInMinute);

            JwtSecurityToken tokenCreate = new JwtSecurityToken(
                    issuer: this._issuer,
                    audience: this._audience,
                    claims: claims,
                    expires: durationInMinute,
                    signingCredentials: creds
                );

            TokenViewModel tokenViewModelEntity = new TokenViewModel();
            tokenViewModelEntity.token = new JwtSecurityTokenHandler().WriteToken(tokenCreate);
            tokenViewModelEntity.expirationTime = durationInMinute.Hour;
            tokenViewModelEntity.tokenType = CommonConstants.TokenType;
            tokenViewModelEntity.refreshToken = await this.GenerateRefreshToken();

            var tokenViewModel = new List<TokenViewModel>();
            tokenViewModel.Add(tokenViewModelEntity);

            return await Task.FromResult(tokenViewModel);
        }

        public async Task<int> GetCompanyToken(string token)
        {
            int companyId = 0;

            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);

            companyId = int.Parse(jwtSecurityToken.Claims.First(x => x.Type == "company").Value);
            return await Task.FromResult(companyId);
        }

        public async Task<IEnumerable<TokenViewModel>> RefreshToken(TokenRequest tokenRequest)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken validateToken;
            TokenViewModel tokenViewModel = new TokenViewModel();
            var current = tokenHandler.ValidateToken(tokenRequest.tokenJwt, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._key)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true
            }, out validateToken);

            var jwtToken = validateToken as JwtSecurityToken;
            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityBaseException("Token Invalid!!");
            else
            {
                var tokenClaims = tokenHandler.ReadJwtToken(tokenRequest.tokenJwt).Claims;
                var resultFound = 0;
                resultFound += tokenClaims.Where(x => x.Value == tokenRequest.userName).ToList().Count();
                resultFound += tokenClaims.Where(x => x.Value == tokenRequest.email).ToList().Count();
                resultFound += tokenClaims.Where(x => x.Value == tokenRequest.userId.ToString()).ToList().Count();
                resultFound += tokenClaims.Where(x => x.Value == tokenRequest.profileId.ToString()).ToList().Count();
                resultFound += tokenClaims.Where(x => x.Value == tokenRequest.companyId.ToString()).ToList().Count();

                if (resultFound == 5)
                    return await this.GenerateToken(tokenRequest);
                else
                    return new List<TokenViewModel>();
            }
        }

    }
}
