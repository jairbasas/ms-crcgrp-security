using Security.Application.Queries.ViewModels.Base;
using Security.Application.Queries.ViewModels;
using Security.Application.Wrappers;

namespace Security.Application.Queries.Interfaces
{
    public interface IProfileQuery
    {
        Task<Response<ProfileViewModel>> GetById(int profileId);

        Task<Response<IEnumerable<ProfileViewModel>>> GetBySearch(ProfileRequest request);

        Task<Response<PaginationViewModel<ProfileViewModel>>> GetByFindAll(ProfileRequest request);
    }
}
