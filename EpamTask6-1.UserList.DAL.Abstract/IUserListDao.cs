namespace EpamTask6_1.UserList.DAL.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using EpamTask6_1.UserList.Entities;

    public interface IUserListDao
    {
        bool AddUser(User user);

        bool DeleteUser(Guid id);

        User GetUser(Guid id);

        IEnumerable<User> GetAllUsers();

        bool SetAllUsers(IEnumerable<User> users);

        public bool SetUserImage(User user);

        public bool GetUserImage(User user);

        public bool RemoveUserImage(User user);

        //bool AddAward(Award award);

        //bool AddAwardToUser(Guid user_id, Guid award_id);

        //Award GetAward(Guid id);

        //bool DeleteUserAwards(User user);

        //IEnumerable<Award> GetUserAwards(User user);

        //IEnumerable<Award> GetAllAwards();

        //IEnumerable<UsersAward> GetAllUserAwards();

        //bool SetAllAwards(IEnumerable<Award> awards);

        //bool SetAllUserAwards(IEnumerable<UsersAward> _usersAndAwardsList);  
    }
}
