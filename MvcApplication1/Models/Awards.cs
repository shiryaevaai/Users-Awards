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

        public Awards() { }

        public Awards(string title)
        {
            this.Title = title;
        }

        public Awards(Guid id, string title)
        {
            this.ID = id;
            this.Title = title;
        }

        public static UserListLogic _logic = new UserListLogic();

        public static IEnumerable<Awards> GetAllAwards()
        {  
            var list = _logic.GetAllAwards();
            foreach (var item in list)
            {
                Awards award = new Awards(item.ID, item.Title);
                yield return award;
            }
        }

        public static Awards GetUAward(Guid id)
        {
            var item = _logic.GetAwardByID(id);
            return new Awards(item.ID, item.Title);
        }

        public static void CreateAward(Awards model)
        {
            _logic.AddAward(model.Title);
        }

    }
}