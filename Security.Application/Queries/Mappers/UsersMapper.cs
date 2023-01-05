using Security.Application.Queries.ViewModels;

namespace Security.Application.Queries.Mappers
{
    public interface IUsersMapper
    {
        UsersViewModel MapToUsersViewModel(dynamic r);
    }

    public class UsersMapper : IUsersMapper
    {
        public UsersViewModel MapToUsersViewModel(dynamic r)
        {
            UsersViewModel o = new UsersViewModel();

            o.userId = r.user_id;
            o.userName = r.user_name;
            o.fatherLastName = r.father_last_name;
            o.motherLastName = r.mother_last_name;
            o.documentNumber = r.document_number;
            o.email = r.email;
            o.login = r.login;
            //o.password = r.password;
            o.resetPassword = r.reset_password;
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
