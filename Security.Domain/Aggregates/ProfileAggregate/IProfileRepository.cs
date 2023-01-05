
namespace Security.Domain.Aggregates.ProfileAggregate
{
    public interface IProfileRepository
    {
        Task<int> Register(Profile profile);
    }
}
