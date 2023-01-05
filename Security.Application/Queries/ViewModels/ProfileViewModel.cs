using Security.Application.Queries.ViewModels.Base;

namespace Security.Application.Queries.ViewModels
{
    public class ProfileViewModel
    {
        public int profileId { get; set; }
        public string profileName { get; set; }
        public int? state { get; set; }
        public int? systemId { get; set; }
        public int? registerUserId { get; set; }
        public string registerUserFullname { get; set; }
        public DateTime? registerDatetime { get; set; }
        public int? updateUserId { get; set; }
        public string updateUserFullname { get; set; }
        public DateTime? updateDatetime { get; set; }
    }

    public class ProfileRequest : PaginationRequest
    {
        public int? profileId { get; set; }
        public int? state { get; set; }
        public int? systemId { get; set; }
    }
}
