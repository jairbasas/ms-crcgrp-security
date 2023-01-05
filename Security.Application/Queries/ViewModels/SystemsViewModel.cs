using Security.Application.Queries.ViewModels.Base;

namespace Security.Application.Queries.ViewModels
{
    public class SystemsViewModel
    {
        public int systemId { get; set; }
        public string systemName { get; set; }
        public int? state { get; set; }
        public int? registerUserId { get; set; }
        public string registerUserFullname { get; set; }
        public DateTime? registerDatetime { get; set; }
        public int? updateUserId { get; set; }
        public string updateUserFullname { get; set; }
        public DateTime? updateDatetime { get; set; }
    }

    public class SystemsRequest : PaginationRequest
    {
        public int systemId { get; set; }
    }
}
