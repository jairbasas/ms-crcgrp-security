using Security.Domain.Core;

namespace Security.Domain.Aggregates.ProfileAggregate
{
    public class Profile : Audit
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

        public Profile()
        {
        }

        public Profile(int profileId, string profileName, int? state, int? systemId, int? registerUserId, string registerUserFullname, DateTime? registerDatetime, int? updateUserId, string updateUserFullname, DateTime? updateDatetime)
        {
            this.profileId = profileId;
            this.profileName = profileName;
            this.state = state;
            this.systemId = systemId;
            this.registerUserId = registerUserId;
            this.registerUserFullname = registerUserFullname;
            this.registerDatetime = registerDatetime;
            this.updateUserId = updateUserId;
            this.updateUserFullname = updateUserFullname;
            this.updateDatetime = updateDatetime;
        }
    }
}
