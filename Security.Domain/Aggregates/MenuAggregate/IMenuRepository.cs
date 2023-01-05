
namespace Security.Domain.Aggregates.MenuAggregate
{
    public interface IMenuRepository
    {
        Task<int> Register(Menu menu);
    }
}
