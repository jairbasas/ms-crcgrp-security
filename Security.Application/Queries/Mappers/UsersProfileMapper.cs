using Security.Application.Queries.ViewModels;

namespace Security.Application.Queries.Mappers
{
    public interface IUsersProfileMapper
    {
        UsersProfileViewModel MapToUsersProfileViewModel(dynamic r);
    }

    public class UsersProfileMapper : IUsersProfileMapper
    {
        public UsersProfileViewModel MapToUsersProfileViewModel(dynamic r)
        {
            UsersProfileViewModel o = new UsersProfileViewModel();

            o.userId = r.user_id;
            o.profileId = r.profile_id;
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
