using Security.Application.Queries.ViewModels.Base;

namespace Security.Application.Queries.ViewModels
{
    public class UsersViewModel
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public string fatherLastName { get; set; }
        public string motherLastName { get; set; }
        public string documentNumber { get; set; }
        public string email { get; set; }
        public string login { get; set; }
        public string? password { get; set; }
        public int? resetPassword { get; set; }
        public int? state { get; set; }
        public string? stateName { get; set; }
        public int? registerUserId { get; set; }
        public string registerUserFullname { get; set; }
        public DateTime? registerDatetime { get; set; }
        public int? updateUserId { get; set; }
        public string updateUserFullname { get; set; }
        public DateTime? updateDatetime { get; set; }
    }

    public class UsersRequest : PaginationRequest
    {
        public int? userId { get; set; }
        public string? userName { get; set; }
        public int? state { get; set; }
    }
}
