using Security.Application.Queries.Generics;
using Security.Application.Queries.Interfaces;
using Security.Application.Queries.Mappers;
using Security.Application.Queries.ViewModels.Base;
using Security.Application.Queries.ViewModels;
using Security.Application.Utility;
using Security.Application.Wrappers;

namespace Security.Application.Queries.Implementations
{
    public class UsersProfileQuery : IUsersProfileQuery
    {
        private readonly IGenericQuery _iGenericQuery;
        private readonly IUsersProfileMapper _iUsersProfileMapper;

        public UsersProfileQuery(IGenericQuery iGenericQuery, IUsersProfileMapper iUsersProfileMapper)
        {
            _iGenericQuery = iGenericQuery ?? throw new ArgumentNullException(nameof(iGenericQuery));
            _iUsersProfileMapper = iUsersProfileMapper ?? throw new ArgumentNullException(nameof(iUsersProfileMapper));
        }

        public async Task<Response<UsersProfileViewModel>> GetById(int userId)
        {
            var parameters = new Dictionary<string, object>
            {
                {"user_id", userId}
            };

            var result = await _iGenericQuery.Search(@"SECURITY.USERS_PROFILE_search", ConvertTo.Xml(parameters));

            var items = (result != null) ? _iUsersProfileMapper.MapToUsersProfileViewModel(result) : null;
            return new Response<UsersProfileViewModel>(items);
        }

        public async Task<Response<IEnumerable<UsersProfileViewModel>>> GetBySearch(UsersProfileRequest request)
        {
            var parameters = new Dictionary<string, object>
            {
                {"user_id", request.userId ?? 0}
            };

            var result = await _iGenericQuery.Search(@"SECURITY.USERS_PROFILE_search", ConvertTo.Xml(parameters), request.pagination);

            var items = result.Select(item => (UsersProfileViewModel)_iUsersProfileMapper.MapToUsersProfileViewModel(item));

            return new Response<IEnumerable<UsersProfileViewModel>>(items);
        }

        public async Task<Response<PaginationViewModel<UsersProfileViewModel>>> GetByFindAll(UsersProfileRequest request)
        {
            var parameters = new Dictionary<string, object>
            {
                {"user_id", request.userId ?? 0}
            };

            var result = await _iGenericQuery.FindAll(@"SECURITY.USERS_PROFILE_find_all", ConvertTo.Xml(parameters), request.pagination);

            var items = result.Select(item => (UsersProfileViewModel)_iUsersProfileMapper.MapToUsersProfileViewModel(item));

            return new Response<PaginationViewModel<UsersProfileViewModel>>(new PaginationViewModel<UsersProfileViewModel>(request.pagination, items));
        }
    }
}
