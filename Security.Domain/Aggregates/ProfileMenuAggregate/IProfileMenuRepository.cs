
namespace Security.Domain.Aggregates.ProfileMenuAggregate
{
    public interface IProfileMenuRepository
    {
        Task<int> Register(ProfileMenu profileMenu);
        Task<int> RegisterAsync(IEnumerable<ProfileMenu> profileMenu);
        Task<int> Delete(ProfileMenu profileMenu);
    }
}
