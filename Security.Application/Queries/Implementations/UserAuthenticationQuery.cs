using Dapper;
using MoreLinq;
using Security.Application.Queries.Generics;
using Security.Application.Queries.Interfaces;
using Security.Application.Queries.Mappers;
using Security.Application.Queries.ViewModels;
using Security.Application.Wrappers;
using Security.Domain.Exceptions;
using System.Data;

namespace Security.Application.Queries.Implementations
{
    public class UserAuthenticationQuery : IUserAuthenticationQuery
    {
        private readonly IUserAuthenticationMapper _iUserAuthenticationMapper;
        private readonly ITokenQuery _iTokenQuery;
        private readonly IGenericQuery _iGenericQuery;
        private readonly IValuesSettings _iValuesSettings;
        private readonly IUsersProfileQuery _iUsersProfileQuery;
        public UserAuthenticationQuery(IGenericQuery igenericQuery, IUserAuthenticationMapper iuserAuthenticationMapper, ITokenQuery itokenQuery, IValuesSettings iValuesSettings, IUsersProfileQuery iUsersProfileQuery)
        {
            this._iGenericQuery = igenericQuery;
            this._iUserAuthenticationMapper = iuserAuthenticationMapper;
            this._iTokenQuery = itokenQuery;
            this._iValuesSettings = iValuesSettings;
            _iUsersProfileQuery = iUsersProfileQuery;
        }

        public async Task<IEnumerable<UserAuthenticationViewModel>> Search(UserAuthenticationRequest userAuthenticationRequest)
        {
            var dynamicParameters = new DynamicParameters();

            //var keybytes = Encoding.UTF8.GetBytes(_iValuesSettings.GetEncryptKey());
            //var iv = Encoding.UTF8.GetBytes(_iValuesSettings.GetEncryptIv());

            //userAuthenticationRequest.password = ConvertTo.DecryptStringFromBytes(Convert.FromBase64String(userAuthenticationRequest.password), keybytes, iv);

            dynamicParameters.Add("pi_login", userAuthenticationRequest.login, DbType.String);
            dynamicParameters.Add("pi_password", userAuthenticationRequest.password, DbType.String);

            var userAuthentication = await _iGenericQuery.ExecuteDirect<dynamic>("SECURITY.USERS_authentication", dynamicParameters);
            return userAuthentication.Select(item => (UserAuthenticationViewModel)_iUserAuthenticationMapper.MapToUserAuthenticationViewModel(item));
        }

        public async Task<Response<UserAuthenticationViewModel>> AuthenticationUserToken(UserAuthenticationRequest userAuthenticationRequest)
        {
            UserAuthenticationViewModel? userAuthenticationViewModel = null;
            var userAuthentication = await Search(userAuthenticationRequest);

            if (userAuthentication.Any())
            {

                var tokenGenerated = await this._iTokenQuery.GenerateToken(new TokenRequest()
                {
                    userName = userAuthentication.FirstOrDefault().userName,
                    email = userAuthentication.FirstOrDefault().email,
                    userId = userAuthentication.FirstOrDefault().userId
                });

                if (tokenGenerated != null)
                {
                    userAuthenticationViewModel = new UserAuthenticationViewModel();

                    userAuthenticationViewModel.userId = userAuthentication.FirstOrDefault().userId;
                    userAuthenticationViewModel.userName = userAuthentication.FirstOrDefault().userName;
                    userAuthenticationViewModel.email = userAuthentication.FirstOrDefault().email;
                    userAuthenticationViewModel.state = userAuthentication.FirstOrDefault().state;
                    userAuthenticationViewModel.tokenUser = tokenGenerated;
                }

                var userProfileResponse = await _iUsersProfileQuery.GetBySearch(new UsersProfileRequest()
                {
                    userId = userAuthentication.FirstOrDefault()?.userId
                });

                if (userProfileResponse.data.Any()) 
                {
                    var profileToken = userAuthenticationViewModel?.profile.ToList();
                    userProfileResponse.data.ForEach(item => {
                        profileToken.Add(new ProfileToken 
                        {
                            profileId = item.profileId
                        });
                    });
                    userAuthenticationViewModel.profile= profileToken;
                }

            }
            else
                throw new SecurityBaseException("Las credenciales proporcionadas, no son válidas");

            return new Response<UserAuthenticationViewModel>(userAuthenticationViewModel);
        }
    }
}
