namespace MvcApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using EpamTask6_1.UserList.Entities;
    using EpamTask6_1.UserList.Logic;

    public class Users
    {
        [Required(ErrorMessage="Необходимо ввести имя пользователя!")]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Необходимо ввести дату рождения пользователя!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата рождения")]
        public DateTime DateOfBirth { get; set; }

        public int Age
        {
            get
            {
                int days, months, years;
                if (DateTime.Now < this.DateOfBirth)
                {
                    throw new ArgumentException("Неверный ввод даты рождения");//
                }
                else
                {
                    years = DateTime.Now.Year - this.DateOfBirth.Year;
                    months = DateTime.Now.Month - this.DateOfBirth.Month;
                    if (months < 0)
                    {
                        months += 12;
                        years--;
                    }

                    days = DateTime.Now.Day - this.DateOfBirth.Day;
                    if (days < 0)
                    {
                        days += DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                        months--;
                        if (months < 0)
                        {
                            months += 12;
                            years--;
                        }

                        if (months >= 12)
                        {
                            months -= 12;
                            years++;
                        }
                    }

                    if ((days < 0) || (months < 0) || (years < 0))
                    {
                        throw new ArgumentException("Неверный ввод даты!");//
                    }

                    if (years > 150)
                    {
                        throw new ArgumentException("Слишком большой возраст");//
                    }
                }

                return years;
            }
        }

        public List<Guid> _awardList = new List<Guid>();

        public Guid ID { get; set; }

        public static UserListLogic _logic = new UserListLogic();

        public Users() { }

        public Users(string name, DateTime dateOfBirth)
        {

            this.Name = name;
            this.DateOfBirth = dateOfBirth;
        }

        public Users(Guid id, string name, DateTime dateOfBirth)
        {
            this.ID = id;
            this.Name = name;
            this.DateOfBirth = dateOfBirth;
        }

        public Users(Guid id, string name, DateTime dateOfBirth, IEnumerable<Guid> inputList) :
            this(id, name, dateOfBirth)
        {
            this.ID = id;
            this.Name = name;
            this.DateOfBirth = dateOfBirth;
            this._awardList = inputList.ToList();
        }

        public static IEnumerable<Users> GetAllUsers()
        {
            var list = _logic.GetAllUsers();
            foreach (var item in list)
            {
                Users user = new Users(item.ID, item.Name, item.DateOfBirth, item.GetAwardList());
                yield return user;
            }
        }

        public static Users GetUser(Guid id)
        {
            var item = _logic.GetUserByID(id);
            Users user = new Users(item.ID, item.Name, item.DateOfBirth, item.GetAwardList());
            return user;
        }

        public static void CreateUser(Users model)
        {
            _logic.AddUser(model.Name, model.DateOfBirth);
        }

        public static void UpdateUser(Users model)
        {
            // _logic.AddUser(model.name, model.dateOfBirth);
        }

        public static void DeleteUser(Users model)
        {
            User nu = _logic.GetUserByID(model.ID);
            _logic.DeleteUser(nu);  
        }

        public static IEnumerable<Awards> GetUserAwards(Guid id)
        {
            User nu = _logic.GetUserByID(id);
            var list = _logic.GetUserAwards(nu);
            foreach (var item in list)
            {
                Awards aw = new Awards(item.ID, item.Title);
                yield return aw;
            }
        } 
    }
}