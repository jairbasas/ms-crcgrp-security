using Security.Application.Queries.ViewModels;

namespace Security.Application.Queries.Mappers
{
    public interface ISystemsMapper
    {
        SystemsViewModel MapToSystemsViewModel(dynamic r);
    }

    public class SystemsMapper : ISystemsMapper
    {
        public SystemsViewModel MapToSystemsViewModel(dynamic r)
        {
            SystemsViewModel o = new SystemsViewModel();

            o.systemId = r.system_id;
            o.systemName = r.system_name;
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
