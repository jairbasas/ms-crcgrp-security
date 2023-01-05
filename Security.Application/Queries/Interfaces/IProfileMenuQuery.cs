using Security.Application.Queries.ViewModels.Base;
using Security.Application.Queries.ViewModels;
using Security.Application.Wrappers;

namespace Security.Application.Queries.Interfaces
{
    public interface IProfileMenuQuery
    {
        Task<Response<ProfileMenuViewModel>> GetById(int profileId);

        Task<Response<IEnumerable<ProfileMenuViewModel>>> GetBySearch(ProfileMenuRequest request);

        Task<Response<PaginationViewModel<ProfileMenuViewModel>>> GetByFindAll(ProfileMenuRequest request);
    }
}
