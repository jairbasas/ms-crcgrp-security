using Security.Application.Queries.ViewModels.Base;
using Security.Application.Queries.ViewModels;
using Security.Application.Wrappers;

namespace Security.Application.Queries.Interfaces
{
    public interface ISystemsQuery
    {
        Task<Response<SystemsViewModel>> GetById(int systemId);

        Task<Response<IEnumerable<SystemsViewModel>>> GetBySearch(SystemsRequest request);

        Task<Response<PaginationViewModel<SystemsViewModel>>> GetByFindAll(SystemsRequest request);
    }
}
