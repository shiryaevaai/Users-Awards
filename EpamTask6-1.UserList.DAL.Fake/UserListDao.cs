namespace EpamTask6_1.UserList.DAL.Fake
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using EpamTask6_1.UserList.DAL.Abstract;
    using EpamTask6_1.UserList.Entities;

    public class UserListDao : IUserListDao
    {
        private Dictionary<Guid, User> _userDictionary;

        public UserListDao()
        {
            this._userDictionary = new Dictionary<Guid, User>();
        }

        public bool AddUser(User user)
        {
            if (this.GetUser(user.ID) != null)
            {
                return false;
            }

            try
            {
                this._userDictionary.Add(user.ID, user); 

                // wrong id, handle exception
                return true;
            }
            catch
            {
                return false;
            }
        }

        public User GetUser(Guid id)
        {
            return this._userDictionary.ContainsKey(id) ? this._userDictionary[id] : null;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return this._userDictionary.Values.ToList();
        }

        public bool DeleteUser(Guid id)
        {
            if (this._userDictionary.ContainsKey(id))
            {
                this._userDictionary.Remove(id);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SetAllUsers(IEnumerable<User> users)
        {
            try
            {
                this._userDictionary.Clear();
                foreach (var user in users)
                {
                    this._userDictionary.Add(user.ID, user);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SetUserImage(Guid id)
        {
            return true;
        }

        public bool GetUserImage(Guid id)
        {
            return true;
        }

        public bool RemoveUserImage(Guid id)
        {
            return true;
        }        
    }
}
