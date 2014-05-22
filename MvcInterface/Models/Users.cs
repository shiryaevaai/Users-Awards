namespace MvcInterface.Models
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
        //{
        //    get
        //    {
        //        int days, months, years;
        //        if (DateTime.Now < this.DateOfBirth)
        //        {
        //            throw new ArgumentException("Неверный ввод даты рождения");//
        //        }
        //        else
        //        {
        //            years = DateTime.Now.Year - this.DateOfBirth.Year;
        //            months = DateTime.Now.Month - this.DateOfBirth.Month;
        //            if (months < 0)
        //            {
        //                months += 12;
        //                years--;
        //            }

        //            days = DateTime.Now.Day - this.DateOfBirth.Day;
        //            if (days < 0)
        //            {
        //                days += DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
        //                months--;
        //                if (months < 0)
        //                {
        //                    months += 12;
        //                    years--;
        //                }

        //                if (months >= 12)
        //                {
        //                    months -= 12;
        //                    years++;
        //                }
        //            }

        //            if ((days < 0) || (months < 0) || (years < 0))
        //            {
        //                throw new ArgumentException("Неверный ввод даты!");//
        //            }

        //            if (years > 150)
        //            {
        //                throw new ArgumentException("Слишком большой возраст");//
        //            }
        //        }

        //        return years;
        //    }
        //}

        public List<Guid> _awardList = new List<Guid>();

        public static List<Awards> _awardNotHasList = new List<Awards>();

        public string DefaultImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "default.jpg");
        //public string DefaultImage ="C:\\Users\\User\\Documents\\Visual Studio 2012\\Projects\\EpamTask6-1\\MvcInterface\\default.jpg";

       // C:\Users\User\Documents\Visual Studio 2012\Projects\EpamTask6-1\MvcInterface

        public string ImageAddr { get; set; }

        public Users() { }

        public Users(string name, DateTime dateOfBirth)
        {
            this.Name = name;
            this.DateOfBirth = dateOfBirth;
            this.ImageAddr = DefaultImage;
        }

        public Users(Guid id, string name, DateTime dateOfBirth, int age)
        {
            this.ID = id;
            this.Name = name;
            this.DateOfBirth = dateOfBirth;
            this.Age = age;
            this.ImageAddr = DefaultImage;
        }

        public Users(Guid id, string name, DateTime dateOfBirth, int age, IEnumerable<Guid> inputList) :
            this(id, name, dateOfBirth, age)
        {
            this.ID = id;
            this.Name = name;
            this.DateOfBirth = dateOfBirth;
            this._awardList = inputList.ToList();
            this.Age = age;
            this.ImageAddr = DefaultImage;
        }

        public static IEnumerable<Users> GetAllUsers()
        {
            var list = BusinessLogicHelper._logic.GetAllUsers();
            foreach (var item in list)
            {
                Users user = new Users(item.ID, item.Name, item.DateOfBirth, item.Age, item.GetAwardList());
                yield return user;
            }
        }

        public static Users GetUser(Guid id)
        {
            var item = BusinessLogicHelper._logic.GetUserByID(id);
            Users user = new Users(item.ID, item.Name, item.DateOfBirth, item.Age, item.GetAwardList());
            return user;
        }

        public static void CreateUser(Users model)
        {
            BusinessLogicHelper._logic.AddUser(model.Name, model.DateOfBirth);
        }

        public static void UpdateUser(Users model)
        {
            // _logic.AddUser(model.name, model.dateOfBirth);
        }

        public static void DeleteUser(Guid id)
        {
            User nu = BusinessLogicHelper._logic.GetUserByID(id);
            BusinessLogicHelper._logic.DeleteUser(nu);
        }

        public static IEnumerable<Awards> GetUserAwards(Guid id)
        {
            User nu = BusinessLogicHelper._logic.GetUserByID(id);
            var list = BusinessLogicHelper._logic.GetUserAwards(nu);
            foreach (var item in list)
            {
                Awards aw = new Awards(item.ID, item.Title);
                yield return aw;
               
            }
        }

        public static IEnumerable<Awards> GetUserNotHasAwards(Guid id)
        {

            User nu = BusinessLogicHelper._logic.GetUserByID(id);
            var list = BusinessLogicHelper._logic.GetUserAwards(nu);
            var all = BusinessLogicHelper._logic.GetAllAwards();
            foreach (var item in all)
            {
                if (!list.Contains(item))
                {
                    Awards aw = new Awards(item.ID, item.Title);
                    _awardNotHasList.Add(aw);
                }
            }

            return _awardNotHasList;
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

        public void SetImage(string imageAddress)
        {
            this.ImageAddr = imageAddress;
        }
        
    }
}