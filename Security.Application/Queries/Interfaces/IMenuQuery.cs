using Security.Application.Queries.ViewModels.Base;
using Security.Application.Queries.ViewModels;
using Security.Application.Wrappers;

namespace Security.Application.Queries.Interfaces
{
    public interface IMenuQuery
    {
        Task<Response<MenuViewModel>> GetById(int menuId);

        Task<Response<IEnumerable<MenuViewModel>>> GetBySearch(MenuRequest request);
        Task<Response<IEnumerable<MenuViewModel>>> GetTreeMenu(MenuRequest request);

        Task<Response<PaginationViewModel<MenuViewModel>>> GetByFindAll(MenuRequest request);
    }
}
