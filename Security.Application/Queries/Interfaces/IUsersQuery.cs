using Security.Application.Queries.ViewModels.Base;
using Security.Application.Queries.ViewModels;
using Security.Application.Wrappers;

namespace Security.Application.Queries.Interfaces
{
    public interface IUsersQuery
    {
        Task<Response<UsersViewModel>> GetById(int userId);

        Task<Response<IEnumerable<UsersViewModel>>> GetBySearch(UsersRequest request);

        Task<Response<PaginationViewModel<UsersViewModel>>> GetByFindAll(UsersRequest request);
    }
}
