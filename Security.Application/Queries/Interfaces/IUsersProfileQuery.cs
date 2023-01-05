using Security.Application.Queries.ViewModels.Base;
using Security.Application.Queries.ViewModels;
using Security.Application.Wrappers;

namespace Security.Application.Queries.Interfaces
{
    public interface IUsersProfileQuery
    {
        Task<Response<UsersProfileViewModel>> GetById(int userId);

        Task<Response<IEnumerable<UsersProfileViewModel>>> GetBySearch(UsersProfileRequest request);

        Task<Response<PaginationViewModel<UsersProfileViewModel>>> GetByFindAll(UsersProfileRequest request);
    }
}
