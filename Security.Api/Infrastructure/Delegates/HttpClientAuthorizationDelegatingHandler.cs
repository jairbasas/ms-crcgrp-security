using Microsoft.AspNetCore.Authentication;

namespace Security.Api.Infrastructure.Delegates
{
    public class HttpClientAuthorizationDelegatingHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccesor;

        public HttpClientAuthorizationDelegatingHandler(IHttpContextAccessor httpContextAccesor)
        {
            _httpContextAccesor = httpContextAccesor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var authorizationHeader = _httpContextAccesor.HttpContext
                .Request.Headers["Authorization"];

            if (!string.IsNullOrEmpty(authorizationHeader))
            {
                request.Headers.Add("Authorization", new List<string>() { authorizationHeader });
            }
            return await base.SendAsync(request, cancellationToken);
        }

        async Task<string> GetToken()
        {
            const string ACCESS_TOKEN = "access_token";

            return await _httpContextAccesor.HttpContext
                .GetTokenAsync(ACCESS_TOKEN);
        }
    }
}
