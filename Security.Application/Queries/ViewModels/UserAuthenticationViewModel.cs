
namespace Security.Application.Queries.ViewModels
{
    public class UserAuthenticationViewModel
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public int state { get; set; }
        public IEnumerable<ProfileToken> profile { get; set; }
        public IEnumerable<TokenViewModel> tokenUser { get; set; }

        public UserAuthenticationViewModel()
        {
            this.tokenUser = new List<TokenViewModel>();
            this.profile = new List<ProfileToken>();
        }
    }

    public class ProfileToken 
    {
        public int profileId { get; set; }
    }

    public class UserAuthenticationRequest
    {
        public string login { get; set; }
        public string password { get; set; }
    }
}
