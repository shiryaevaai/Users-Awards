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

    public class Awards
    {
        public static Dictionary<Guid, string> _awardsImageList = new Dictionary<Guid, string>();

        public static string DefaultImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "AwardImages", "default.jpg");

        [Required]
        [Display(Name = "Название")]
        [Remote("CheckAwardName", "Awards")]
        public string Title { get; set; }
        
        [HiddenInput(DisplayValue = false)]
        public Guid ID { get; set; }

        public string ImageAddr { get; set; }

        public Awards() { }

        public Awards(string title)
        {
            this.Title = title;
            this.ImageAddr = DefaultImage;            
        }

        public Awards(Guid id, string title)
        {
            this.ID = id;
            this.Title = title;
            this.ImageAddr = DefaultImage;
        }

        public static IEnumerable<Awards> GetAllAwards()
        {
            var list = BusinessLogicHelper._logic.GetAllAwards();
            foreach (var item in list)
            {                
                Awards award = new Awards(item.ID, item.Title);
                if (Awards._awardsImageList.ContainsKey(item.ID))
                {
                    award.SetImage();
                }
                yield return award;
            }
        }

        public static Awards GetAward(Guid id)
        {
            var item = BusinessLogicHelper._logic.GetAwardByID(id);
            Awards award = new Awards(item.ID, item.Title);
            if (Awards._awardsImageList.ContainsKey(item.ID))
            {
                award.SetImage();
            }
            return award;
        }
 
        public static void CreateAward(Awards model)
        {
            BusinessLogicHelper._logic.AddAward(model.Title);
        }

        public static bool CheckAwardTitle(string title)
        {
            var list = BusinessLogicHelper._logic.GetAllAwards();
            foreach (var aw in list)
            {
                if (aw.Title == title)
                {
                    return false;
                }
            }
            return true;
        }

        public void SetImage()
        {
            if (!Awards._awardsImageList.ContainsKey(this.ID))
            {
                Awards._awardsImageList.Add(this.ID, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "AwardImages", this.ID.ToString()));
                this.ImageAddr = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "AwardImages", this.ID.ToString());
            }
            else
            {
                Awards._awardsImageList[this.ID] = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "AwardImages", this.ID.ToString());
                this.ImageAddr = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "AwardImages", this.ID.ToString());
            }
        }

        public void RemoveImage()
        {
            if (Awards._awardsImageList.ContainsKey(this.ID))
            {
                Awards._awardsImageList.Remove(this.ID);
            }

            this.ImageAddr = Awards.DefaultImage;

        }


    }
}