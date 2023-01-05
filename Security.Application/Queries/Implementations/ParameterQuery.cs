using Security.Application.Queries.Generics;
using Security.Application.Queries.Interfaces;
using Security.Application.Queries.Mappers;
using Security.Application.Queries.ViewModels.Base;
using Security.Application.Queries.ViewModels;
using Security.Application.Utility;
using Security.Application.Wrappers;

namespace Security.Application.Queries.Implementations
{
    public class ParameterQuery : IParameterQuery
    {
        private readonly IGenericQuery _iGenericQuery;
        private readonly IParameterMapper _iParameterMapper;

        public ParameterQuery(IGenericQuery iGenericQuery, IParameterMapper iParameterMapper)
        {
            _iGenericQuery = iGenericQuery ?? throw new ArgumentNullException(nameof(iGenericQuery));
            _iParameterMapper = iParameterMapper ?? throw new ArgumentNullException(nameof(iParameterMapper));
        }

        public async Task<Response<ParameterViewModel>> GetById(int parameterId)
        {
            var parameters = new Dictionary<string, object>
            {
                {"parameter_id", parameterId}
            };

            var result = await _iGenericQuery.Search(@"SECURITY.PARAMETER_search", ConvertTo.Xml(parameters));

            var items = (result != null) ? _iParameterMapper.MapToParameterViewModel(result) : null;
            return new Response<ParameterViewModel>(items);
        }

        public async Task<Response<IEnumerable<ParameterViewModel>>> GetBySearch(ParameterRequest request)
        {
            var parameters = new Dictionary<string, object>
            {
                {"parameter_id", request.parameterId}
            };

            var result = await _iGenericQuery.Search(@"SECURITY.PARAMETER_search", ConvertTo.Xml(parameters), request.pagination);

            var items = result.Select(item => (ParameterViewModel)_iParameterMapper.MapToParameterViewModel(item));

            return new Response<IEnumerable<ParameterViewModel>>(items);
        }

        public async Task<Response<PaginationViewModel<ParameterViewModel>>> GetByFindAll(ParameterRequest request)
        {
            var parameters = new Dictionary<string, object>
            {
                {"parameter_id", request.parameterId}
            };

            var result = await _iGenericQuery.FindAll(@"SECURITY.PARAMETER_find_all", ConvertTo.Xml(parameters), request.pagination);

            var items = result.Select(item => (ParameterViewModel)_iParameterMapper.MapToParameterViewModel(item));

            return new Response<PaginationViewModel<ParameterViewModel>>(new PaginationViewModel<ParameterViewModel>(request.pagination, items));
        }
    }
}
