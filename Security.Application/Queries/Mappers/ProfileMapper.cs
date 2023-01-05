using Security.Application.Queries.ViewModels;

namespace Security.Application.Queries.Mappers
{
    public interface IProfileMapper
    {
        ProfileViewModel MapToProfileViewModel(dynamic r);
    }

    public class ProfileMapper : IProfileMapper
    {
        public ProfileViewModel MapToProfileViewModel(dynamic r)
        {
            ProfileViewModel o = new ProfileViewModel();

            o.profileId = r.profile_id;
            o.profileName = r.profile_name;
            o.state = r.state;
            o.systemId = r.system_id;
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
