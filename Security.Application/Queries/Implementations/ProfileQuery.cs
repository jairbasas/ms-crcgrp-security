using Security.Application.Queries.Generics;
using Security.Application.Queries.Interfaces;
using Security.Application.Queries.Mappers;
using Security.Application.Queries.ViewModels.Base;
using Security.Application.Queries.ViewModels;
using Security.Application.Utility;
using Security.Application.Wrappers;

namespace Security.Application.Queries.Implementations
{
    public class ProfileQuery : IProfileQuery
    {
        private readonly IGenericQuery _iGenericQuery;
        private readonly IProfileMapper _iProfileMapper;

        public ProfileQuery(IGenericQuery iGenericQuery, IProfileMapper iProfileMapper)
        {
            _iGenericQuery = iGenericQuery ?? throw new ArgumentNullException(nameof(iGenericQuery));
            _iProfileMapper = iProfileMapper ?? throw new ArgumentNullException(nameof(iProfileMapper));
        }

        public async Task<Response<ProfileViewModel>> GetById(int profileId)
        {
            var parameters = new Dictionary<string, object>
            {
                {"profile_id", profileId}
            };

            var result = await _iGenericQuery.Search(@"SECURITY.PROFILE_search", ConvertTo.Xml(parameters));

            var items = (result != null) ? _iProfileMapper.MapToProfileViewModel(result) : null;
            return new Response<ProfileViewModel>(items);
        }

        public async Task<Response<IEnumerable<ProfileViewModel>>> GetBySearch(ProfileRequest request)
        {
            var parameters = new Dictionary<string, object>
            {
                {"profile_id", request.profileId ?? 0},
                {"system_id", request.systemId ?? 0},
                {"state", request.state ?? 0}
            };

            var result = await _iGenericQuery.Search(@"SECURITY.PROFILE_search", ConvertTo.Xml(parameters), request.pagination);

            var items = result.Select(item => (ProfileViewModel)_iProfileMapper.MapToProfileViewModel(item));

            return new Response<IEnumerable<ProfileViewModel>>(items);
        }

        public async Task<Response<PaginationViewModel<ProfileViewModel>>> GetByFindAll(ProfileRequest request)
        {
            var parameters = new Dictionary<string, object>
            {
                {"profile_id", request.profileId ?? 0}
            };

            var result = await _iGenericQuery.FindAll(@"SECURITY.PROFILE_find_all", ConvertTo.Xml(parameters), request.pagination);

            var items = result.Select(item => (ProfileViewModel)_iProfileMapper.MapToProfileViewModel(item));

            return new Response<PaginationViewModel<ProfileViewModel>>(new PaginationViewModel<ProfileViewModel>(request.pagination, items));
        }
    }
}
