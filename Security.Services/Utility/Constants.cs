
namespace Security.Services.Utility
{
    public class Constants
    {
        public struct EndpointEmployees
        {
            public const string GetCompanyUserByUserId = "/company-users/search?userId={0}";
            public const string PostCompanyUsers = "/company-users";
        }
    }
}
