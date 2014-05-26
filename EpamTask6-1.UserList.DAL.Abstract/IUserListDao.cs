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

        bool SetUserImage(Guid id);

        bool GetUserImage(Guid id);

        bool RemoveUserImage(Guid id);
         
    }
}
