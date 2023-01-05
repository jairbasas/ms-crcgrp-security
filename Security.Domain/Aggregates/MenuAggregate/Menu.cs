using Security.Domain.Core;

namespace Security.Domain.Aggregates.MenuAggregate
{
    public class Menu : Audit
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
        public string registerUserFullname { get; set; }
        public DateTime? registerDatetime { get; set; }
        public int? updateUserId { get; set; }
        public string updateUserFullname { get; set; }
        public DateTime updateDatetime { get; set; }

        public Menu()
        {
        }

        public Menu(int menuId, string menuName, int? level, string url, string icon, int? order, int? menuParentId, int? state, int? registerUserId, string registerUserFullname, DateTime? registerDatetime, int? updateUserId, string updateUserFullname, DateTime updateDatetime)
        {
            this.menuId = menuId;
            this.menuName = menuName;
            this.level = level;
            this.url = url;
            this.icon = icon;
            this.order = order;
            this.menuParentId = menuParentId;
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
