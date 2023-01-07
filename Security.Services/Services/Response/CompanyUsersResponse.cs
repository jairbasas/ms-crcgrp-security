using Security.Services.Helper;

namespace Security.Services.Services.Response
{
    public class CompanyUsersResponse : BaseResponse
    {
        public IEnumerable<CompanyUsers> data { get; set; }
    }

    public class CompanyUsers 
    {
        public int companyUserId { get; set; }
        public int? userId { get; set; }
        public int? companyId { get; set; }
        public int? registerUserId { get; set; }
        public string registerUserFullname { get; set; }
        public DateTime? registerDatetime { get; set; }
        public int? updateUserId { get; set; }
        public string updateUserFullname { get; set; }
        public DateTime? updateDatetime { get; set; }
        public int? state { get; set; }
    }

}
