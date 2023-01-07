
namespace Security.Services.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<T> GetCompanyUsersByUserId<T>(int userId);
        Task<T> CreateCompanyUsers<T>(string jsonPost);
    }
}
