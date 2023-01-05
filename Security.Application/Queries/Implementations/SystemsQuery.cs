using Security.Application.Queries.Generics;
using Security.Application.Queries.Interfaces;
using Security.Application.Queries.Mappers;
using Security.Application.Queries.ViewModels.Base;
using Security.Application.Queries.ViewModels;
using Security.Application.Utility;
using Security.Application.Wrappers;

namespace Security.Application.Queries.Implementations
{
    public class SystemsQuery : ISystemsQuery
    {
        private readonly IGenericQuery _iGenericQuery;
        private readonly ISystemsMapper _iSystemsMapper;

        public SystemsQuery(IGenericQuery iGenericQuery, ISystemsMapper iSystemsMapper)
        {
            _iGenericQuery = iGenericQuery ?? throw new ArgumentNullException(nameof(iGenericQuery));
            _iSystemsMapper = iSystemsMapper ?? throw new ArgumentNullException(nameof(iSystemsMapper));
        }

        public async Task<Response<SystemsViewModel>> GetById(int systemId)
        {
            var parameters = new Dictionary<string, object>
            {
                {"system_id", systemId}
            };

            var result = await _iGenericQuery.Search(@"SECURITY.SYSTEMS_search", ConvertTo.Xml(parameters));

            var items = (result != null) ? _iSystemsMapper.MapToSystemsViewModel(result) : null;
            return new Response<SystemsViewModel>(items);
        }

        public async Task<Response<IEnumerable<SystemsViewModel>>> GetBySearch(SystemsRequest request)
        {
            var parameters = new Dictionary<string, object>
            {
                {"system_id", request.systemId}
            };

            var result = await _iGenericQuery.Search(@"SECURITY.SYSTEMS_search", ConvertTo.Xml(parameters), request.pagination);

            var items = result.Select(item => (SystemsViewModel)_iSystemsMapper.MapToSystemsViewModel(item));

            return new Response<IEnumerable<SystemsViewModel>>(items);
        }

        public async Task<Response<PaginationViewModel<SystemsViewModel>>> GetByFindAll(SystemsRequest request)
        {
            var parameters = new Dictionary<string, object>
            {
                {"system_id", request.systemId}
            };

            var result = await _iGenericQuery.FindAll(@"SECURITY.SYSTEMS_find_all", ConvertTo.Xml(parameters), request.pagination);

            var items = result.Select(item => (SystemsViewModel)_iSystemsMapper.MapToSystemsViewModel(item));

            return new Response<PaginationViewModel<SystemsViewModel>>(new PaginationViewModel<SystemsViewModel>(request.pagination, items));
        }
    }
}
