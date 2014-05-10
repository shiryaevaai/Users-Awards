﻿namespace EpamTask6_1.UserList.DAL.Files
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using EpamTask6_1.UserList.DAL.Abstract;
    using EpamTask6_1.UserList.Entities;

    public class AwardListDao : IAwardListDao
    {
        private const char SEPARATOR_CHAR = '▄';
        private const string SEPARATOR_STRING = "▄";
        
        private const string AWARDS_FILE = "awards.txt";
        private const string AWARDS_AND_USERS_FILE = "awards_and_users.txt";

        private string _awardsFile;
        private string _awards_and_usersFile;

        public AwardListDao()
        {

            this._awardsFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AWARDS_FILE);
            this._awards_and_usersFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AWARDS_AND_USERS_FILE);
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

    }
}
