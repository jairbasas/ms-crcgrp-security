using Security.Application.Queries.Generics;
using Security.Application.Queries.Interfaces;
using Security.Application.Queries.Mappers;
using Security.Application.Queries.ViewModels.Base;
using Security.Application.Queries.ViewModels;
using Security.Application.Utility;
using Security.Application.Wrappers;

namespace Security.Application.Queries.Implementations
{
    public class ParameterDetailQuery : IParameterDetailQuery
    {
        private readonly IGenericQuery _iGenericQuery;
        private readonly IParameterDetailMapper _iParameterDetailMapper;

        public ParameterDetailQuery(IGenericQuery iGenericQuery, IParameterDetailMapper iParameterDetailMapper)
        {
            _iGenericQuery = iGenericQuery ?? throw new ArgumentNullException(nameof(iGenericQuery));
            _iParameterDetailMapper = iParameterDetailMapper ?? throw new ArgumentNullException(nameof(iParameterDetailMapper));
        }

        public async Task<Response<ParameterDetailViewModel>> GetById(int parameterDetailId)
        {
            var parameters = new Dictionary<string, object>
            {
                {"parameter_detail_id", parameterDetailId}
            };

            var result = await _iGenericQuery.Search(@"SECURITY.PARAMETER_DETAIL_search", ConvertTo.Xml(parameters));

            var items = (result != null) ? _iParameterDetailMapper.MapToParameterDetailViewModel(result) : null;
            return new Response<ParameterDetailViewModel>(items);
        }

        public async Task<Response<IEnumerable<ParameterDetailViewModel>>> GetBySearch(ParameterDetailRequest request)
        {
            var parameters = new Dictionary<string, object>
            {
                {"parameter_detail_id", request.parameterDetailId ?? 0},
                {"parameter_id", request.parameterId ?? 0}
            };

            var result = await _iGenericQuery.Search(@"SECURITY.PARAMETER_DETAIL_search", ConvertTo.Xml(parameters), request.pagination);

            var items = result.Select(item => (ParameterDetailViewModel)_iParameterDetailMapper.MapToParameterDetailViewModel(item));

            return new Response<IEnumerable<ParameterDetailViewModel>>(items);
        }

        public async Task<Response<PaginationViewModel<ParameterDetailViewModel>>> GetByFindAll(ParameterDetailRequest request)
        {
            var parameters = new Dictionary<string, object>
            {
                {"parameter_detail_id", request.parameterDetailId ?? 0}
            };

            var result = await _iGenericQuery.FindAll(@"SECURITY.PARAMETER_DETAIL_find_all", ConvertTo.Xml(parameters), request.pagination);

            var items = result.Select(item => (ParameterDetailViewModel)_iParameterDetailMapper.MapToParameterDetailViewModel(item));

            return new Response<PaginationViewModel<ParameterDetailViewModel>>(new PaginationViewModel<ParameterDetailViewModel>(request.pagination, items));
        }
    }
}
