using Security.Application.Queries.ViewModels;

namespace Security.Application.Queries.Mappers
{
    public interface IMenuMapper
    {
        MenuViewModel MapToMenuViewModel(dynamic r);
    }

    public class MenuMapper : IMenuMapper
    {
        public MenuViewModel MapToMenuViewModel(dynamic r)
        {
            MenuViewModel o = new MenuViewModel();

            o.menuId = r.menu_id;
            o.menuName = r.menu_name;
            o.level = r.level;
            o.url = r.url;
            o.icon = r.icon;
            o.order = r.order;
            o.menuParentId = r.menu_parent_id;
            o.state = r.state;
            o.registerUserId = r.register_user_id;
            o.registerUserFullname = r.register_user_fullname;
            o.registerDatetime = r.register_datetime;
            o.updateUserId = r.update_user_id;
            o.updateUserFullname = r.update_user_fullname;
            o.updateDatetime = r.update_datetime;

            return o;
        }
    }
}
