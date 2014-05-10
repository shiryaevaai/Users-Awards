namespace EpamTask6_1.UserList.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class UsersAward
    {
        public Guid UserID { get; set; }

        public Guid AwardID { get; set; }

        public UsersAward(Guid userID, Guid awardID)
        {
            this.UserID = userID;
            this.AwardID = awardID;
        }

        public UsersAward(User user, Award award)
        {
            this.UserID = user.ID;
            this.AwardID = award.ID;
        }

    }
}
