
namespace Security.Domain.Core
{
    public class Audit
    {
        public int? registerUserId { get; set; }
        public string? registerUserFullname { get; set; }
        public DateTime? registerDatetime { get; set; }
        public int? updateUserId { get; set; }
        public string? updateUserFullname { get; set; }
        public DateTime? updateDatetime { get; set; }
    }
}
