
namespace Security.Domain.Aggregates.SystemsAggregate
{
    public interface ISystemsRepository
    {
        Task<int> Register(Systems systems);
    }
}
