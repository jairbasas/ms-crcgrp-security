using Security.Application.Queries.ViewModels;

namespace Security.Application.Queries.Mappers
{
    public interface IUserAuthenticationMapper
    {
        UserAuthenticationViewModel MapToUserAuthenticationViewModel(dynamic r);
    }
    public class UserAuthenticationMapper : IUserAuthenticationMapper
    {
        public UserAuthenticationViewModel MapToUserAuthenticationViewModel(dynamic r)
        {
            UserAuthenticationViewModel o = new UserAuthenticationViewModel();

            o.userId = r.user_id;
            o.userName = r.user_name + ' ' + r.father_last_name + ' ' + r.mother_last_name;
            o.email = r.email;
            o.state = r.state;

            return o;
        }
    }
}
