using Security.Application.Queries.Generics;
using Security.Application.Queries.Interfaces;
using Security.Application.Queries.Mappers;
using Security.Application.Queries.ViewModels.Base;
using Security.Application.Queries.ViewModels;
using Security.Application.Utility;
using Security.Application.Wrappers;

namespace Security.Application.Queries.Implementations
{
    public class MenuQuery : IMenuQuery
    {
        private readonly IGenericQuery _iGenericQuery;
        private readonly IMenuMapper _iMenuMapper;
        private readonly IProfileMenuQuery _iProfileMenuQuery;

        public MenuQuery(IGenericQuery iGenericQuery, IMenuMapper iMenuMapper, IProfileMenuQuery iProfileMenuQuery)
        {
            _iGenericQuery = iGenericQuery ?? throw new ArgumentNullException(nameof(iGenericQuery));
            _iMenuMapper = iMenuMapper ?? throw new ArgumentNullException(nameof(iMenuMapper));
            _iProfileMenuQuery = iProfileMenuQuery;
        }

        public async Task<Response<MenuViewModel>> GetById(int menuId)
        {
            var parameters = new Dictionary<string, object>
            {
                {"menu_id", menuId}
            };

            var result = await _iGenericQuery.Search(@"SECURITY.MENU_search", ConvertTo.Xml(parameters));

            var items = (result != null) ? _iMenuMapper.MapToMenuViewModel(result) : null;
            return new Response<MenuViewModel>(items);
        }

        public async Task<Response<IEnumerable<MenuViewModel>>> GetBySearch(MenuRequest request)
        {
            var parameters = new Dictionary<string, object>
            {
                {"menu_id", request.menuId ?? 0},
                {"menu_id_list", request.menuIdList ?? string.Empty},
                {"state", request.state ?? 0}
            };

            var result = await _iGenericQuery.Search(@"SECURITY.MENU_search", ConvertTo.Xml(parameters), request.pagination);

            var items = result.Select(item => (MenuViewModel)_iMenuMapper.MapToMenuViewModel(item));

            return new Response<IEnumerable<MenuViewModel>>(items);
        }

        public async Task<Response<PaginationViewModel<MenuViewModel>>> GetByFindAll(MenuRequest request)
        {
            var parameters = new Dictionary<string, object>
            {
                {"menu_id", request.menuId ?? 0}
            };

            var result = await _iGenericQuery.FindAll(@"SECURITY.MENU_find_all", ConvertTo.Xml(parameters), request.pagination);

            var items = result.Select(item => (MenuViewModel)_iMenuMapper.MapToMenuViewModel(item));

            return new Response<PaginationViewModel<MenuViewModel>>(new PaginationViewModel<MenuViewModel>(request.pagination, items));
        }

        public async Task<Response<IEnumerable<MenuViewModel>>> GetTreeMenu(MenuRequest request)
        {
            var menuResponse = new List<MenuViewModel>();
            var menuByProfileList = await _iProfileMenuQuery.GetBySearch(new ProfileMenuRequest() 
            {
                profileId = request.profileId,
            });

            var menuList = await GetBySearch(new MenuRequest() 
            {
                state = StateEntity.Active
            });

            if (menuByProfileList.data.Any())
            {
                var menuParent = getParentMenu(menuList.data.Where(x => x.menuParentId == null), menuByProfileList.data);
                var childMenu = getChildMenu(menuList.data.Where(x => x.menuParentId != null), menuByProfileList.data);
                menuResponse = AssociateMenuParentChild(menuParent, childMenu);
            }
            return new Response<IEnumerable<MenuViewModel>>(menuResponse);
        }

        #region Methods

        private IEnumerable<MenuViewModel> getParentMenu(IEnumerable<MenuViewModel> menuList, IEnumerable<ProfileMenuViewModel> profileMenuList)
        {
            var parentMenuList = new List<MenuViewModel>();

            foreach (var item in profileMenuList)
            {
                var parentMenuFound = menuList.Where(x => x.menuId == item.menuId).FirstOrDefault();
                if (parentMenuFound != null) parentMenuList.Add(parentMenuFound);
            }

            parentMenuList = parentMenuList.OrderBy(x => x.order).ToList();

            return parentMenuList;
        }

        private IEnumerable<MenuViewModel> getChildMenu(IEnumerable<MenuViewModel> menuList, IEnumerable<ProfileMenuViewModel> profileMenuList)
        {
            var childMenuList = new List<MenuViewModel>();

            foreach (var item in profileMenuList)
            {
                var childMenuFound = menuList.Where(x => x.menuId == item.menuId).FirstOrDefault();
                if (childMenuFound != null) childMenuList.Add(childMenuFound);
            }

            return childMenuList;
        }

        private List<MenuViewModel> AssociateMenuParentChild(IEnumerable<MenuViewModel> parentMenu, IEnumerable<MenuViewModel> childMenu)
        {
            var parentChildMenu = new List<MenuViewModel>();

            foreach (var itemParent in parentMenu)
            {
                var childMenuFound = childMenu.Where(x => x.menuParentId == itemParent.menuId);
                itemParent.childMenu = new List<ChildMenuViewModel>();

                if (childMenuFound.Any())
                {
                    childMenuFound = childMenuFound.OrderBy(x => x.order);
                    foreach (var itemChild in childMenuFound)
                    {
                        itemParent.childMenu.Add(new ChildMenuViewModel()
                        {
                            menuId = itemChild.menuId,
                            icon = itemChild.icon,
                            level = itemChild.level.Value,
                            menuName = itemChild.menuName,
                            menuParentId = itemChild.menuParentId,
                            order = itemChild.order.Value,
                            state = itemChild.state.Value,
                            url = itemChild.url,
                            registerUserId = itemChild.registerUserId.Value,
                            registerUserFullname = itemChild.registerUserFullname,
                            registerDatetime = itemChild.registerDatetime,
                            updateUserId = itemChild.updateUserId,
                            updateUserFullname = itemChild.updateUserFullname,
                            updateDatetime = itemChild.updateDatetime
                        });
                    }
                    parentChildMenu.Add(itemParent);
                }
                else
                    parentChildMenu.Add(itemParent);

            }

            return parentChildMenu;
        }

        #endregion

    }
}
