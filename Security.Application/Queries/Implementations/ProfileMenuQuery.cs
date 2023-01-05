using Security.Application.Queries.Generics;
using Security.Application.Queries.Interfaces;
using Security.Application.Queries.Mappers;
using Security.Application.Queries.ViewModels.Base;
using Security.Application.Queries.ViewModels;
using Security.Application.Utility;
using Security.Application.Wrappers;

namespace Security.Application.Queries.Implementations
{
    public class ProfileMenuQuery : IProfileMenuQuery
    {
        private readonly IGenericQuery _iGenericQuery;
        private readonly IProfileMenuMapper _iProfileMenuMapper;

        public ProfileMenuQuery(IGenericQuery iGenericQuery, IProfileMenuMapper iProfileMenuMapper)
        {
            _iGenericQuery = iGenericQuery ?? throw new ArgumentNullException(nameof(iGenericQuery));
            _iProfileMenuMapper = iProfileMenuMapper ?? throw new ArgumentNullException(nameof(iProfileMenuMapper));
        }

        public async Task<Response<ProfileMenuViewModel>> GetById(int profileId)
        {
            var parameters = new Dictionary<string, object>
            {
                {"profile_id", profileId}
            };

            var result = await _iGenericQuery.Search(@"SECURITY.PROFILE_MENU_search", ConvertTo.Xml(parameters));

            var items = (result != null) ? _iProfileMenuMapper.MapToProfileMenuViewModel(result) : null;
            return new Response<ProfileMenuViewModel>(items);
        }

        public async Task<Response<IEnumerable<ProfileMenuViewModel>>> GetBySearch(ProfileMenuRequest request)
        {
            var parameters = new Dictionary<string, object>
            {
                {"profile_id", request.profileId ?? 0}
            };

            var result = await _iGenericQuery.Search(@"SECURITY.PROFILE_MENU_search", ConvertTo.Xml(parameters), request.pagination);

            var items = result.Select(item => (ProfileMenuViewModel)_iProfileMenuMapper.MapToProfileMenuViewModel(item));

            return new Response<IEnumerable<ProfileMenuViewModel>>(items);
        }

        public async Task<Response<PaginationViewModel<ProfileMenuViewModel>>> GetByFindAll(ProfileMenuRequest request)
        {
            var parameters = new Dictionary<string, object>
            {
                {"profile_id", request.profileId ?? 0}
            };

            var result = await _iGenericQuery.FindAll(@"SECURITY.PROFILE_MENU_find_all", ConvertTo.Xml(parameters), request.pagination);

            var items = result.Select(item => (ProfileMenuViewModel)_iProfileMenuMapper.MapToProfileMenuViewModel(item));

            return new Response<PaginationViewModel<ProfileMenuViewModel>>(new PaginationViewModel<ProfileMenuViewModel>(request.pagination, items));
        }
    }
}
