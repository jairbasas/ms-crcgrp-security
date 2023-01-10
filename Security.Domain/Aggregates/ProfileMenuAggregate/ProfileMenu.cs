using Security.Domain.Core;

namespace Security.Domain.Aggregates.ProfileMenuAggregate
{
    public class ProfileMenu : Audit
    {
        public int profileId { get; set; }
        public int menuId { get; set; }
        public int[] menuIds { get; set; }
        public int? registerUserId { get; set; }
        public string registerUserFullname { get; set; }
        public DateTime? registerDatetime { get; set; }
        public int? updateUserId { get; set; }
        public string updateUserFullname { get; set; }
        public DateTime? updateDatetime { get; set; }

        public ProfileMenu()
        {
        }

        public ProfileMenu(int profileId, int menuId, int? registerUserId, string registerUserFullname, DateTime? registerDatetime, int? updateUserId, string updateUserFullname, DateTime? updateDatetime, int[] menuIds)
        {
            this.profileId = profileId;
            this.menuId = menuId;
            this.registerUserId = registerUserId;
            this.registerUserFullname = registerUserFullname;
            this.registerDatetime = registerDatetime;
            this.updateUserId = updateUserId;
            this.updateUserFullname = updateUserFullname;
            this.updateDatetime = updateDatetime;
            this.menuIds = menuIds;
        }

        public ProfileMenu(int profileId, int menuId)
        {
            this.profileId = profileId;
            this.menuId = menuId;
        }
    }
}
