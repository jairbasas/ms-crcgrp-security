using Security.Application.Queries.ViewModels.Base;

namespace Security.Application.Queries.ViewModels
{
    public class UsersProfileViewModel
    {
        public int userId { get; set; }
        public int profileId { get; set; }
        public int? registerUserId { get; set; }
        public string registerUserFullname { get; set; }
        public DateTime? registerDatetime { get; set; }
        public int? updateUserId { get; set; }
        public string updateUserFullname { get; set; }
        public DateTime? updateDatetime { get; set; }
    }

    public class UsersProfileRequest : PaginationRequest
    {
        public int? userId { get; set; }
        public int? profileId { get; set; }
        public int? systemId { get; set; }
    }
}
