namespace MvcApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    //using EpamTask6_1.UserList.DAL.Abstract;
    //using EpamTask6_1.UserList.DAL.Fake;
    //using EpamTask6_1.UserList.DAL.Files;
    using EpamTask6_1.UserList.Entities;
    using EpamTask6_1.UserList.Logic;

    public class Users
    {
        [Required]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "dd/mm/yyyy", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата рождения")]
        public DateTime DateOfBirth { get; set; }

        public List<Guid> _awardList = new List<Guid>();

        public Guid ID { get; set; }

        public static UserListLogic _logic = new UserListLogic();

        //public Users(string name, DateTime dateOfBirth)
        //{
        //    User nu = new User(name, dateOfBirth);
        //    this.ID = nu.ID;
        //    this.name = nu.Name;
        //    this.dateOfBirth = nu.DateOfBirth;
        //}

        //public Users(string name, DateTime dateOfBirth, IEnumerable<Guid> inputList) :
        //    this(name, dateOfBirth)
        //{
        //    User nu = new User(name, dateOfBirth, inputList);
        //    this.ID = nu.ID;
        //    this.name = nu.Name;
        //    this.dateOfBirth = nu.DateOfBirth;
        //    this._awardList = nu.GetAwardList();
        //}

        public static IEnumerable<User> GetAllUsers()
        {
            return _logic.GetAllUsers();
        }

        public static User GetUser(Guid id)
        {
            return _logic.GetUserByID(id);
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
    }
}