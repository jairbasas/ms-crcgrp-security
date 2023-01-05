
namespace Security.Domain.Aggregates.UsersProfileAggregate
{
    public interface IUsersProfileRepository
    {
        Task<int> Register(UsersProfile usersProfile);
    }
}
