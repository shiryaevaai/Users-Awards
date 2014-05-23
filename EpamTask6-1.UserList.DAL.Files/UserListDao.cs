namespace EpamTask6_1.UserList.DAL.Files
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using EpamTask6_1.UserList.DAL.Abstract;
    using EpamTask6_1.UserList.Entities;

    public class UserListDao : IUserListDao
    {
        private const char SEPARATOR_CHAR = '▄';
        private const string SEPARATOR_STRING = "▄";

        private const string USERS_FILE = "users.txt";
        private const string AWARDS_FILE = "awards.txt";
        private const string AWARDS_AND_USERS_FILE = "awards_and_users.txt";
        private const string USERS_IMAGE_FILE = "users_image.txt";
       
        private string _usersFile;
        private string _awardsFile;
        private string _awards_and_usersFile;
        private string _users_imageFile;

        public UserListDao()
        {           

            this._usersFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, USERS_FILE);
            this._awardsFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AWARDS_FILE);
            this._awards_and_usersFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AWARDS_AND_USERS_FILE);
            this._users_imageFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, USERS_IMAGE_FILE);
        }

        public bool AddUser(User user)
        {
            // wrong id, handle exception
            if (this.GetUser(user.ID) != null)
            {
                return false;
            }

            try
            {

                File.AppendAllLines(this._usersFile, new[] { CreateLineFromUser(user) });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public User GetUser(Guid id)
        {
            return this.GetAllUsers().FirstOrDefault(n => n.ID == id);
        }

        public IEnumerable<User> GetAllUsers()
        {
            //if (!File.Exists(this._usersFile))
            //{
            //    throw new FileNotFoundException("Входной файл не найден!");
            //}

            string[] lines = File.ReadAllLines(this._usersFile);  
            
            // reader, every string 
            foreach (string line in lines)
            {
                var user = CreateUserFromLine(line);
                if (user != null)
                {
                    yield return user;
                }
            }
        }

        public IEnumerable<UserImage> GetAllImages()
        {
            string[] lines = File.ReadAllLines(this._users_imageFile);
 
            foreach (string line in lines)
            {
                var userImage = CreateUserImageFromLine(line);
                if (userImage != null)
                {
                    yield return userImage;
                }
            }
        }

        public bool DeleteUser(Guid id)
        {
            try
            {
                if (!File.Exists(this._usersFile))
                {
                    return false;
                }

                var users = this.GetAllUsers()
                    .Where(n => n.ID != id)
                    .Select(n => CreateLineFromUser(n));
                // .Select(CreateLineFromUser);

                File.WriteAllLines(this._usersFile, users);

                // var ua = GetAllUserAwards()
                //    .Where(n => n.Value.Key == id)
                //    .Select(n => n);                               

                // return SetAllUserAwards(ua.ToDictionary(n=>n.Key));
                return true;                
            }
            catch
            {
                return false;
            }
        }

        public bool SetAllUsers(IEnumerable<User> users)
        {
            try
            {
                List<string> resLines = new List<string>();
                foreach (var user in users)
                {
                    resLines.Add(CreateLineFromUser(user));
                }

                File.WriteAllLines(this._usersFile, resLines);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddAward(Award award)
        {
            if (this.GetAward(award.ID) != null)
            {
                return false;
            }

            try
            {
                File.AppendAllLines(this._awardsFile, new[] { CreateLineFromAward(award) }); 
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Award GetAward(Guid id)
        {
            return this.GetAllAwards().FirstOrDefault(n => n.ID == id);
        }

        public IEnumerable<Award> GetAllAwards()
        {
            if (!File.Exists(this._awardsFile))
            {
                throw new FileNotFoundException("Входной файл не найден");
            }

            string[] lines = File.ReadAllLines(this._awardsFile);
            foreach (string line in lines)
            {
                var award = CreateAwardFromLine(line);
                if (award != null)
                {
                    yield return award;
                }
            }
        }

        public IEnumerable<Award> GetUserAwards(User user)
        {
            if (!File.Exists(this._awards_and_usersFile))
            {
                throw new FileNotFoundException("Входной файл не найден");
            }

            string[] lines = File.ReadAllLines(this._awards_and_usersFile);
            foreach (string line in lines)
            {
                var usersAward = line.Split(SEPARATOR_CHAR);
                Guid userId = Guid.Parse(usersAward[1]);

                if (userId == user.ID)
                {
                    Guid awardId = Guid.Parse(usersAward[2]);
                    yield return this.GetAward(awardId);
                }
            }
        }

        public bool AddAwardToUser(Guid userID, Guid awardID) 
        {
            Guid newID = Guid.NewGuid();

            try
            {
                if (!File.Exists(this._awards_and_usersFile))
                {
                    return false;
                }

                File.AppendAllLines(this._awards_and_usersFile, new[] { CreateLineForUsersAward(userID, awardID) }); 
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SetAllAwards(IEnumerable<Award> awards)
        {
            try
            {
                List<string> resLines = new List<string>();
                foreach (var res in awards)
                {
                    resLines.Add(res.ID.ToString() + SEPARATOR_STRING + res.Title.ToString());
                }

                File.WriteAllLines(this._awardsFile, resLines);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SetAllUserAwards(IEnumerable<UsersAward> _usersAndAwardsList)
        {
            try
            {
                List<string> resLines = new List<string>();
                foreach (var res in _usersAndAwardsList)
                {
                    resLines.Add(res.UserID.ToString() + SEPARATOR_STRING + res.AwardID.ToString());
                }

                File.WriteAllLines(this._awards_and_usersFile, resLines);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<UsersAward> GetAllUserAwards()
        {
            if (!File.Exists(this._awards_and_usersFile))
            {
                throw new FileNotFoundException("Входной файл не найден");
            }

            List<UsersAward> res = new List<UsersAward>();
            string[] lines = File.ReadAllLines(this._awards_and_usersFile);
            foreach (string line in lines)
            {
                res.Add(CreateUsersAwardFromLine(line));
            }

            return res;
        }

        public bool DeleteUserAwards(User user)
        {
            try
            {
                var usersAndAwards = this.GetAllUserAwards()
                    .Where(n => n.UserID != user.ID)
                    .Select(n => n);

                List<string> resLines = new List<string>();
                foreach (var res in usersAndAwards)
                {
                    resLines.Add(CreateLineForUsersAward(res.UserID, res.AwardID));
                }

                File.WriteAllLines(this._awards_and_usersFile, resLines);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SetUserImage(Guid id)
        {
            // wrong id, handle exception
            if (this.GetUser(id) == null)
            {
                return false;
            }

            try
            {
                RemoveUserImage(id);
                File.AppendAllLines(this._users_imageFile, new[] { CreateImageLineFromUserImage(id) });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool GetUserImage(Guid id)
        {
            // wrong id, handle exception
            if (this.GetUser(id) == null)
            {
                return false;
            }

            try
            {
                var list = this.GetAllImages()
                    .Where(n => n.UserID == id)
                    .Select(n => n);
                if (list.Count() !=0 )
                {
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

        public bool RemoveUserImage(Guid id)
        {
            // wrong id, handle exception
            if (this.GetUser(id) == null)
            {
                return false;
            }
            ///?????
            try
            {
                if (this.GetUserImage(id))
                {
                    var users = this.GetAllImages()
                        .Where(n => n.UserID != id)                       
                        .Select(n => CreateLineFromUserImage(n));

                    var users2 = this.GetAllImages()
                       .Where(n => n.UserID == id)
                       .Select(n => n.Image);  ///?????
                    // .Select(n => CreateLineFromUserImage(n));
 
                    File.WriteAllLines(this._users_imageFile, users);
                }

                return true;
            }
            catch
            {
                return false;
            }

        }

        private static string CreateLineForUsersAward(Guid userID, Guid awardID)
        {
            return string.Format("{0}{1}{2}", userID.ToString(), SEPARATOR_STRING, awardID.ToString());
        }

        private static UsersAward CreateUsersAwardFromLine(string line)
        {
            var fields = line.Split(SEPARATOR_CHAR);
            if (fields.Length != 2)
            {
                return null;
            }

            return new UsersAward(Guid.Parse(fields[0]), Guid.Parse(fields[1]));
        }

        private static string CreateLineFromAward(Award award)
        {
            return string.Format("{0}{1}{2}", award.ID.ToString(), SEPARATOR_STRING, award.Title.ToString());
        }

        private static Award CreateAwardFromLine(string line)
        {
            var awardFields = line.Split(SEPARATOR_CHAR);
            if (awardFields.Length != 2)
            {
                return null;
            }

            return new Award(awardFields[1])
            {
                ID = Guid.Parse(awardFields[0]),
            };
        }

        private static string CreateLineFromUser(User user)
        {
            return string.Format(
                "{0}{3}{1}{3}{2}", 
                user.ID.ToString(), 
                user.Name.ToString(),
                user.DateOfBirth.ToString(), 
                SEPARATOR_STRING);
        }

        private static User CreateUserFromLine(string line)
        {
            var userFields = line.Split(SEPARATOR_CHAR);
            if (userFields.Length != 3)
            {
                return null;
            }

            return new User(userFields[1], DateTime.Parse(userFields[2]))
            {
                ID = Guid.Parse(userFields[0]),
            };
        }

        private static string CreateLineFromUserImage(UserImage userImage)
        {
            return string.Format(
                "{0}{1}{2}",
                userImage.UserID.ToString(),
                SEPARATOR_STRING,
                //Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "UserImages", user.ID.ToString()));
                userImage.Image);
        }

        private static string CreateImageLineFromUserImage(Guid id)
        {
            return string.Format(
                "{0}{1}{2}",
                id.ToString(),
                SEPARATOR_STRING,
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "UserImages", id.ToString()));              
        }

        private static UserImage CreateUserImageFromLine(string line)
        {
            var userFields = line.Split(SEPARATOR_CHAR);
            if (userFields.Length != 2)
            {
                return null;
            }

            return new UserImage(Guid.Parse(userFields[0]), userFields[1]);
        }


    }
}
