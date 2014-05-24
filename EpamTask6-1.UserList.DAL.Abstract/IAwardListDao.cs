using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EpamTask6_1.UserList.Entities;

namespace EpamTask6_1.UserList.DAL.Abstract
{
    public interface IAwardListDao
    {

        bool AddAward(Award award);

        bool AddAwardToUser(Guid user_id, Guid award_id);

        Award GetAward(Guid id);

        bool DeleteUserAwards(User user);

        IEnumerable<Award> GetUserAwards(User user);

        IEnumerable<Award> GetAllAwards();

        IEnumerable<UsersAward> GetAllUserAwards();

        bool SetAllAwards(IEnumerable<Award> awards);

        bool SetAllUserAwards(IEnumerable<UsersAward> _usersAndAwardsList);

        bool SetAwardImage(Guid id);

        bool GetAwardImage(Guid id);

        bool RemoveAwardImage(Guid id);

    }
}
