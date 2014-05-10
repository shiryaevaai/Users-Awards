﻿namespace EpamTask6_1.UserList.DAL.Fake
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using EpamTask6_1.UserList.DAL.Abstract;
    using EpamTask6_1.UserList.Entities;

    public class AwardListDao : IAwardListDao
    {
        private Dictionary<Guid, Award> _awardDictionary;

        private List<UsersAward> _usersAndAwards;

        public AwardListDao()
        {           
            this._awardDictionary = new Dictionary<Guid, Award>();
            _usersAndAwards = new List<UsersAward>();
        }

        public bool AddAward(Award award)
        {
            if (this.GetAward(award.ID) != null)
            {
                return false;
            }

            try
            {
                this._awardDictionary.Add(award.ID, award);

                // wrong id, handle exception
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Award GetAward(Guid id)
        {
            return this._awardDictionary.ContainsKey(id) ? this._awardDictionary[id] : null;
        }

        public IEnumerable<Award> GetAllAwards()
        {
            return this._awardDictionary.Values.ToList();
        }

        public IEnumerable<Award> GetUserAwards(User user)
        {
            foreach (var r in this._usersAndAwards)
            {
                if (r.UserID == user.ID)
                {
                    yield return this.GetAward(r.AwardID);
                }
            }
        }

        public bool AddAwardToUser(Guid userID, Guid awardID)
        {
            Guid newID = Guid.NewGuid();

            try
            {
                this._usersAndAwards.Add(new UsersAward(userID, awardID));
                // wrong id, handle exception
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SetAllAwards(IEnumerable<Award> awards)
        {
            this._awardDictionary.Clear();
            try
            {
                foreach (var award in awards)
                {
                    this._awardDictionary.Add(award.ID, award);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SetAllUserAwards(IEnumerable<UsersAward> _usersAndAwards)
        {
            this._usersAndAwards.Clear();
            try
            {
                foreach (var kvp in _usersAndAwards)
                {
                    this._usersAndAwards.Add(kvp);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<UsersAward> GetAllUserAwards()
        {
            foreach (var item in this._usersAndAwards)
            {
                yield return item;
            }
        }

        public bool DeleteUserAwards(User user)
        {
            try
            {
                var usersAndAwards = new List<UsersAward>();
                foreach (var item in this._usersAndAwards)
                {
                    if (item.UserID != user.ID)
                    {
                        usersAndAwards.Add(item);
                    }
                }

                this._usersAndAwards.Clear();
                foreach (var ua in usersAndAwards)
                {
                    this._usersAndAwards.Add(ua);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }  
    }
}
