using Security.Services.Helper;
using Security.Services.Services.Interfaces;
using static Security.Services.Utility.Constants;

namespace Security.Services.Services.Implementations
{
    public class EmployeeService : BaseService, IEmployeeService
    {
        public EmployeeService(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<T> CreateCompanyUsers<T>(string jsonPost)
        {
            var url = EndpointEmployees.PostCompanyUsers;
            return await PostServiceEntity<T>(url, jsonPost);
        }

        public async Task<T> GetCompanyUsersByUserId<T>(int userId)
        {
            var url = string.Format(EndpointEmployees.GetCompanyUserByUserId, userId);
            return await CallServiceList<T>(url);
        }
    }
}
