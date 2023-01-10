using Security.Application.Queries.ViewModels;

namespace Security.Application.Queries.Mappers
{
    public interface IProfileMenuMapper
    {
        ProfileMenuViewModel MapToProfileMenuViewModel(dynamic r);
    }

    public class ProfileMenuMapper : IProfileMenuMapper
    {
        public ProfileMenuViewModel MapToProfileMenuViewModel(dynamic r)
        {
            ProfileMenuViewModel o = new ProfileMenuViewModel();

            o.profileId = r.profile_id;
            o.profileName = r.profile_name;
            o.menuId = r.menu_id;
            o.menuName= r.menu_name;
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
