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

    public class Awards
    {
        [Required]
        [Display(Name = "Название")]
        public string Title { get; set; }

        public Guid ID { get; set; }

        public static UserListLogic _logic = new UserListLogic();

        public static IEnumerable<Award> GetAllAwards()
        {
            return _logic.GetAllAwards();
        }

        public static Award GetUAward(Guid id)
        {
            return _logic.GetAwardByID(id);
        }

        public static void CreateAward(Awards model)
        {
            _logic.AddAward(model.Title);
        }

    }
}