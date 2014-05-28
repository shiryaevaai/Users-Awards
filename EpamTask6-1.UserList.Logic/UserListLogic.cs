namespace EpamTask6_1.UserList.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    using EpamTask6_1.UserList.DAL.Abstract;
    using EpamTask6_1.UserList.DAL.DB;
    using EpamTask6_1.UserList.DAL.Fake;
    using EpamTask6_1.UserList.DAL.Files;
    using EpamTask6_1.UserList.Entities;

    public class UserListLogic 
    {
        private IUserListDao _cash_user_dao;

        private IUserListDao _save_user_dao;

        private IAwardListDao _cash_award_dao;

        private IAwardListDao _save_award_dao;

        private IRolesDao _save_roles_dao;

        // Add config parameter to check dal
        public UserListLogic()
        {
#if DEBUG
            this._save_user_dao = new DAL.Files.UserListDao();
            this._cash_user_dao = new DAL.Fake.UserListDao();
            StartCashing();
#else
            //this._save_user_dao = new DAL.Files.UserListDao();
            //this._cash_user_dao = new DAL.Fake.UserListDao();
            //this._save_award_dao = new DAL.Files.AwardListDao();
            //this._cash_award_dao = new DAL.Fake.AwardListDao();

            
            this._cash_user_dao = new DAL.Fake.UserListDao();           
            this._cash_award_dao = new DAL.Fake.AwardListDao();
            try
            {
                this._save_user_dao = new DAL.DB.UserListDao();
                this._save_award_dao = new DAL.DB.AwardListDao();
                this._save_roles_dao = new DAL.DB.RolesDao();
            }
            catch (Exception ex)
            {
                throw new Exception("", ex);
            }

            StartCashing();
#endif
        }

        public bool AddUser(string name, DateTime dateOfBirth)
        {
            var user = new User(name, dateOfBirth);
            if (this._save_user_dao.AddUser(user))
            {
                //Thread cashThread = new Thread(this.UsersCashing);
                //cashThread.Start(); 
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddUser(User user)
        {
            if (this._save_user_dao.AddUser(user))
            {
                //Thread cashThread = new Thread(this.UsersCashing);
                //cashThread.Start();
                return true;
            }
            else
            {
                return false;
            }
        }

        public User GetUserByID(Guid id)
        {
            return this._save_user_dao.GetUser(id);
        }

        public User[] GetAllUsers()
        {
            return this._save_user_dao.GetAllUsers().ToArray(); 
        }


        public bool DeleteUser(User user)
        {
            //if ((this._cash_user_dao.DeleteUser(user.ID))&&(this._cash_award_dao.DeleteUserAwards(user)))
            //{                
            //    Thread cashThread1 = new Thread(this.UsersCashing);
            //    cashThread1.Start();

            //    Thread cashThread2 = new Thread(this.UsersAndAwardsCashing);
            //    cashThread2.Start();
            //    return true;
            //}
            if (this._save_user_dao.DeleteUser(user.ID)) 
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddAward(string title)
        {
            var award = new Award(title);

            if (this._save_award_dao.AddAward(award))
            {
                //Thread cashThread = new Thread(this.AwardsCashing);
                //cashThread.Start();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddAward(Award award)
        {
            if (this._save_award_dao.AddAward(award))
            {
                //Thread cashThread = new Thread(this.AwardsCashing);
                //cashThread.Start();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Award GetAwardByID(Guid id)
        {
            return this._save_award_dao.GetAward(id);
        }

        public Award[] GetAllAwards()
        {
            return this._save_award_dao.GetAllAwards().ToArray(); 
        }

        public Award[] GetUserAwards(User user)
        {
            return this._save_award_dao.GetUserAwards(user).ToArray();
        }

        public bool AddAwardToUser(Guid userID, Guid awardID) 
        {
            if (this._save_user_dao.GetUser(userID) == null)
            {
                throw new ArgumentException("Пользователя с данным ID не существует");
            }

            if (this._save_award_dao.GetAward(awardID) == null)
           {
               throw new ArgumentException("Награды с данным ID не существует");
           }

            if (this._save_award_dao.AddAwardToUser(userID, awardID))
            {
                //Thread cashThread = new Thread(this.UsersAndAwardsCashing);
                //cashThread.Start();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void StartCashing()
        {           
           this._cash_award_dao.SetAllAwards(this._save_award_dao.GetAllAwards());            
            
           this._cash_award_dao.SetAllUserAwards(this._save_award_dao.GetAllUserAwards());            

           this._cash_user_dao.SetAllUsers(this._save_user_dao.GetAllUsers());
            
        }

        public void AwardsCashing()
        {
            if (!this._save_award_dao.SetAllAwards(this._cash_award_dao.GetAllAwards()))
            {
                throw new Exception("Не удалось загрузить информацию о наградах. Приложение будет закрыто.");
            }
        }

        public void UsersCashing()
        {
            if (!this._save_user_dao.SetAllUsers(this._cash_user_dao.GetAllUsers()))
            {
                throw new Exception("Не удалось загрузить информацию о пользователях. Приложение будет закрыто.");
            }
        }

        public void UsersAndAwardsCashing()
        {
            if (!this._save_award_dao.SetAllUserAwards(this._cash_award_dao.GetAllUserAwards()))
            {
                throw new Exception("Не удалось загрузить информацию о наградах пользователей. Приложение будет закрыто.");
            }
        }

        public bool SetUserImage(Guid userID)
        {
            if (this._save_user_dao.GetUser(userID) == null)
            {
                throw new ArgumentException("Пользователя с данным ID не существует");
            }

            if (this._save_user_dao.SetUserImage(userID))
            {
                //Thread cashThread = new Thread(this.UsersAndAwardsCashing);///////
                //cashThread.Start();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool GetUserImage(Guid userID)
        {
            if (this._save_user_dao.GetUser(userID) == null)
            {
                throw new ArgumentException("Пользователя с данным ID не существует");
            }

            try
            {
                if (this._save_user_dao.GetUserImage(userID))
                {
                    //Thread cashThread = new Thread(this.UsersAndAwardsCashing);///////
                    //cashThread.Start();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

        }

        public bool RemoveUserImage(Guid userID)
        {
            if (this._save_user_dao.GetUser(userID) == null)
            {
                throw new ArgumentException("Пользователя с данным ID не существует");
            }

            if (this._save_user_dao.RemoveUserImage(userID))
            {
               // Thread cashThread = new Thread(this.UsersAndAwardsCashing); //////
               // cashThread.Start();
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool SetAwardImage(Guid awardID)
        {
            if (this._save_award_dao.GetAward(awardID) == null)
            {
                throw new ArgumentException("Награды с данным ID не существует");
            }

            if (this._save_award_dao.SetAwardImage(awardID))
            {
                //Thread cashThread = new Thread(this.UsersAndAwardsCashing);///////
                //cashThread.Start();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool GetAwardImage(Guid awardID)
        {
            if (this._save_award_dao.GetAward(awardID) == null)
            {
                throw new ArgumentException("Награды с данным ID не существует");
            }

            if (this._save_award_dao.GetAwardImage(awardID))
            {
                //Thread cashThread = new Thread(this.UsersAndAwardsCashing);///////
                //cashThread.Start();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RemoveAwardImage(Guid awardID)
        {
            if (this._save_award_dao.GetAward(awardID) == null)
            {
                throw new ArgumentException("Награды с данным ID не существует");
            }

            if (this._save_award_dao.RemoveAwardImage(awardID))
            {
                // Thread cashThread = new Thread(this.UsersAndAwardsCashing); //////
                // cashThread.Start();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CreateAccount(Account account)
        {
            if (this._save_roles_dao.AddAccount(account))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddRoleToAccount(System.Guid AccountID, System.Guid RoleID)
        {
            if (this._save_roles_dao.AddRoleToAccount(AccountID, RoleID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Account GetAccount(System.Guid id)
        {
            return this._save_roles_dao.GetAccount(id);
        }

        public Account GetAccount(string username)
        {
            return this._save_roles_dao.GetAccount(username);
        }

        public Role GetRole(System.Guid id)
        {
            return this._save_roles_dao.GetRole(id);
        }

        public System.Collections.Generic.IEnumerable<Role> GetAccountRoles(Account account)
        {
            return this._save_roles_dao.GetAccountRoles(account).ToArray(); 
        }

        public System.Collections.Generic.IEnumerable<Role> GetNoAccountRoles(Account account)
        {
            return this._save_roles_dao.GetNoAccountRoles(account).ToArray(); 
        }

        public System.Collections.Generic.IEnumerable<Role> GetAllRoles()
        {
            return this._save_roles_dao.GetAllRoles().ToArray(); 
        }

        public System.Collections.Generic.IEnumerable<Account> GetAllAccounts()
        {
            return this._save_roles_dao.GetAllAccounts().ToArray();
        }

        public bool DeleteRoleFromAccount(Guid AccountID, Guid RoleID)
        {
            return this._save_roles_dao.DeleteRoleFromAccount(AccountID, RoleID);
        }
    }
}
