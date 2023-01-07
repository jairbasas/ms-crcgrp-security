
namespace Security.Application.Queries.ViewModels
{
    public class TokenViewModel
    {
        public string token { get; set; }
        public string tokenType { get; set; }
        public string refreshToken { get; set; }
        public int expirationTime { get; set; }
    }

    public class TokenRequest
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string tokenJwt { get; set; }
        public string refreshToken { get; set; }
        public int profileId { get; set; }
        public int companyId { get; set; }
    }
}
