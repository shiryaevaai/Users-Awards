﻿namespace MvcInterface.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using EpamTask6_1.UserList.Entities;
    using EpamTask6_1.UserList.Logic;

    using MvcInterface.Models;

    public class Users
    {
        public static string DefaultImage = UserImage.DefaultUserImage;

        public static string ImageDirectory= UserImage.UserImageDirectory;

        [HiddenInput(DisplayValue = false)]
        public Guid ID { get; set; }

        [Required(ErrorMessage = "Необходимо ввести имя пользователя!")]
        [Display(Name = "Имя")]
        [Remote("CheckUserName", "Users")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Необходимо ввести дату рождения пользователя!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата рождения")]
        public DateTime DateOfBirth { get; set; }

        public int Age { get; set; }

        public string ImageAddr { get; set; }
       
        public List<Guid> _awardList = new List<Guid>();

        public List<Awards> _awardNotHasList = new List<Awards>();
        
        public Users() { }

        public Users(string name, DateTime dateOfBirth)
        {
            this.Name = name;
            this.DateOfBirth = dateOfBirth;
            this.ImageAddr = Path.Combine(Users.ImageDirectory, Users.DefaultImage);
        }

        public Users(Guid id, string name, DateTime dateOfBirth, int age)
        {
            this.ID = id;
            this.Name = name;
            this.DateOfBirth = dateOfBirth;
            this.Age = age;
            //this.ImageAddr = DefaultImage;
            this.ImageAddr = Path.Combine(Users.ImageDirectory, Users.DefaultImage);
        }

        public Users(Guid id, string name, DateTime dateOfBirth, int age, IEnumerable<Guid> inputList) :
            this(id, name, dateOfBirth, age)
        {
            this.ID = id;
            this.Name = name;
            this.DateOfBirth = dateOfBirth;
            this._awardList = inputList.ToList();
            this.Age = age;
            //this.ImageAddr = DefaultImage;
            this.ImageAddr = Path.Combine(Users.ImageDirectory, Users.DefaultImage);
        }

        public static IEnumerable<Users> GetAllUsers()
        {
            var list = BusinessLogicHelper._logic.GetAllUsers();
            foreach (var item in list)
            {
                Users user = new Users(item.ID, item.Name, item.DateOfBirth, item.Age, item.GetAwardList());
                if (BusinessLogicHelper._logic.GetUserImage(user.ID))
                {
                    //user.ImageAddr = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "UserImages", user.ID.ToString());
                    user.ImageAddr = Path.Combine(Users.ImageDirectory, user.ID.ToString());
                }
                else
                {
                    //user.ImageAddr = Users.DefaultImage ;
                    user.ImageAddr = Path.Combine(Users.ImageDirectory, DefaultImage);
                }
                yield return user;
            }
        }

        public static Users GetUser(Guid id)
        {
            var item = BusinessLogicHelper._logic.GetUserByID(id);
            Users user = new Users(item.ID, item.Name, item.DateOfBirth, item.Age, item.GetAwardList());

            if (BusinessLogicHelper._logic.GetUserImage(user.ID))
            {
                //user.ImageAddr = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "UserImages", user.ID.ToString());
                user.ImageAddr = Path.Combine(Users.ImageDirectory, user.ID.ToString());
            }
            else
            {
                //user.ImageAddr = Users.DefaultImage ;
                user.ImageAddr = Path.Combine(Users.ImageDirectory, DefaultImage);
            }
            user.GetUserNotHasAwards(user.ID);
            return user;
        }

        public static void CreateUser(Users model)
        {
            BusinessLogicHelper._logic.AddUser(model.Name, model.DateOfBirth);
        }

        public static void DeleteUser(Guid id)
        {
            User nu = BusinessLogicHelper._logic.GetUserByID(id);
            BusinessLogicHelper._logic.DeleteUser(nu);
        }

        public static bool AddAwardToUser(Guid UserID, Guid AwardID)
        {
            User nu = BusinessLogicHelper._logic.GetUserByID(UserID);
            return BusinessLogicHelper._logic.AddAwardToUser(UserID, AwardID);
        }

        public static IEnumerable<Awards> GetUserAwards(Guid id)
        {
            User nu = BusinessLogicHelper._logic.GetUserByID(id);
            var list = BusinessLogicHelper._logic.GetUserAwards(nu);
            foreach (var item in list)
            {
                Awards award = new Awards(item.ID, item.Title);
                if (BusinessLogicHelper._logic.GetAwardImage(award.ID))
                {
                    //award.ImageAddr = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "AwardImages", award.ID.ToString());
                    award.ImageAddr = Path.Combine(Awards.ImageDirectory, award.ID.ToString());
                }
                else
                {
                    //award.ImageAddr = Awards.DefaultImage;
                     award.ImageAddr = Path.Combine(Awards.ImageDirectory, Awards.DefaultImage);
                }
                yield return award;
               
            }
        }

        public void GetUserNotHasAwards(Guid id)
        {
            this._awardNotHasList.Clear();
            User nu = BusinessLogicHelper._logic.GetUserByID(id);
            var list = BusinessLogicHelper._logic.GetUserAwards(nu);
            var all = BusinessLogicHelper._logic.GetAllAwards();
            foreach (var item in all)
            {
                if (!list.Contains(item))
                {
                    Awards award = new Awards(item.ID, item.Title);
                    if (BusinessLogicHelper._logic.GetAwardImage(award.ID))
                    {
                        //award.ImageAddr = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "AwardImages", award.ID.ToString());
                         award.ImageAddr = Path.Combine(Awards.ImageDirectory, award.ID.ToString());
                    }
                    else
                    {
                        //award.ImageAddr = Awards.DefaultImage;
                         award.ImageAddr = Path.Combine(Awards.ImageDirectory, Awards.DefaultImage);
                    }
                    this._awardNotHasList.Add(award);
                }
            }

            //return _awardNotHasList;
        }

        public static bool CheckUserName(string username)
        {
            var list = BusinessLogicHelper._logic.GetAllUsers();
            foreach (var user in list)
            {
                if (user.Name == username)
                {
                    return false;
                }
            }
            return true;
        }

        public void SetImage()
        {
            //if (!Users._usersImageList.ContainsKey(this.ID))
            //{
            //    Users._usersImageList.Add(this.ID, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "UserImages", this.ID.ToString()));
            //    this.ImageAddr = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "UserImages", this.ID.ToString());
            //}
            //else
            //{
            //    Users._usersImageList[this.ID] = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "UserImages", this.ID.ToString());
            //    this.ImageAddr = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "UserImages", this.ID.ToString());
            //}
            BusinessLogicHelper._logic.SetUserImage(this.ID);
            this.ImageAddr = Path.Combine(Users.ImageDirectory, this.ID.ToString());
        }

        public void RemoveImage()
        {
            //if (Users._usersImageList.ContainsKey(this.ID))
            //{
            //    Users._usersImageList.Remove(this.ID);
            //}
            
            //this.ImageAddr = Users.DefaultImage;
            BusinessLogicHelper._logic.RemoveUserImage(this.ID);
            this.ImageAddr = Path.Combine(Users.ImageDirectory, Users.DefaultImage);
        
        }
        
    }
}