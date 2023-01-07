
namespace Security.Services.Services.Body
{
    public class CreateCompanyUsers
    {
        public int companyUserId { get; set; }
        public int? userId { get; set; }
        public int? companyId { get; set; }
        public int? state { get; set; }
        public int? registerUserId { get; set; }
        public string registerUserFullname { get; set; }
    }
}
