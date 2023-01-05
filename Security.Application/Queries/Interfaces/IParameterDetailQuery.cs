using Security.Application.Queries.ViewModels.Base;
using Security.Application.Queries.ViewModels;
using Security.Application.Wrappers;

namespace Security.Application.Queries.Interfaces
{
    public interface IParameterDetailQuery
    {
        Task<Response<ParameterDetailViewModel>> GetById(int parameterDetailId);

        Task<Response<IEnumerable<ParameterDetailViewModel>>> GetBySearch(ParameterDetailRequest request);

        Task<Response<PaginationViewModel<ParameterDetailViewModel>>> GetByFindAll(ParameterDetailRequest request);
    }
}
