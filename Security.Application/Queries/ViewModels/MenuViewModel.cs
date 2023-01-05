using Security.Application.Queries.ViewModels.Base;

namespace Security.Application.Queries.ViewModels
{
    public class MenuViewModel
    {
        public int menuId { get; set; }
        public string menuName { get; set; }
        public int? level { get; set; }
        public string url { get; set; }
        public string icon { get; set; }
        public int? order { get; set; }
        public int? menuParentId { get; set; }
        public int? state { get; set; }
        public int? registerUserId { get; set; }
        public string? registerUserFullname { get; set; }
        public DateTime? registerDatetime { get; set; }
        public int? updateUserId { get; set; }
        public string? updateUserFullname { get; set; }
        public DateTime? updateDatetime { get; set; }
        public List<ChildMenuViewModel> childMenu { get; set; }
    }

    public class MenuRequest : PaginationRequest
    {
        public int? menuId { get; set; }
        public int? profileId { get; set; }
        public int? systemId { get; set; }
        public int? state { get; set; }
        public string? menuIdList { get; set; }
    }
}
