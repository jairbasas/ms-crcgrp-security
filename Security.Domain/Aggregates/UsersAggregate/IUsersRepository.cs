
namespace Security.Domain.Aggregates.UsersAggregate
{
    public interface IUsersRepository
    {
        Task<int> Register(Users users);
        Task<int> ChangePassword(Users users);
        Task<int> ChangeState(Users users);
    }
}
