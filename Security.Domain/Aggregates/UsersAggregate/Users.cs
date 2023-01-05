using Security.Domain.Core;

namespace Security.Domain.Aggregates.UsersAggregate
{
    public class Users : Audit
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
        public int? registerUserId { get; set; }
        public string registerUserFullname { get; set; }
        public DateTime? registerDatetime { get; set; }
        public int? updateUserId { get; set; }
        public string updateUserFullname { get; set; }
        public DateTime? updateDatetime { get; set; }

        public Users()
        {
        }

        public Users(int userId, string userName, string fatherLastName, string motherLastName, string documentNumber, string email, string login, string? password, int? resetPassword, int? state, int? registerUserId, string registerUserFullname, DateTime? registerDatetime, int? updateUserId, string updateUserFullname, DateTime? updateDatetime)
        {
            this.userId = userId;
            this.userName = userName;
            this.fatherLastName = fatherLastName;
            this.motherLastName = motherLastName;
            this.documentNumber = documentNumber;
            this.email = email;
            this.login = login;
            this.password = password;
            this.resetPassword = resetPassword;
            this.state = state;
            this.registerUserId = registerUserId;
            this.registerUserFullname = registerUserFullname;
            this.registerDatetime = registerDatetime;
            this.updateUserId = updateUserId;
            this.updateUserFullname = updateUserFullname;
            this.updateDatetime = updateDatetime;
        }
    }
}
