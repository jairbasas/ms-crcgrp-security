using Security.Application.Queries.ViewModels.Base;
using Security.Application.Queries.ViewModels;
using Security.Application.Wrappers;

namespace Security.Application.Queries.Interfaces
{
    public interface IParameterQuery
    {
        Task<Response<ParameterViewModel>> GetById(int parameterId);

        Task<Response<IEnumerable<ParameterViewModel>>> GetBySearch(ParameterRequest request);

        Task<Response<PaginationViewModel<ParameterViewModel>>> GetByFindAll(ParameterRequest request);
    }
}
