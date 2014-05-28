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

    public class Awards : IEquatable<Awards>
    {
        public static string ImageDirectory = AwardImage.AwardImageDirectory;

        public static string DefaultImage = AwardImage.DefaultAwardImage;

        [Required]
        [Display(Name = "Название")]
        [Remote("CheckAwardName", "Awards")]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Длина строки должна быть от 1 до 255 символов")]
        public string Title { get; set; }
        
        [HiddenInput(DisplayValue = false)]
        public Guid ID { get; set; }

        public string ImageAddr { get; set; }

        public Awards() { }

        public Awards(string title)
        {
            this.Title = title;
            this.ImageAddr = Path.Combine(Awards.ImageDirectory, Awards.DefaultImage);          
        }

        public Awards(Guid id, string title)
        {
            this.ID = id;
            this.Title = title;
           // this.ImageAddr = DefaultImage;
            this.ImageAddr = Path.Combine(Awards.ImageDirectory, Awards.DefaultImage);    
        }

        public static IEnumerable<Awards> GetAllAwards()
        {
            var list = BusinessLogicHelper._logic.GetAllAwards();
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

        public static Awards GetAward(Guid id)
        {
            var item = BusinessLogicHelper._logic.GetAwardByID(id);
            Awards award = new Awards(item.ID, item.Title);
            if (BusinessLogicHelper._logic.GetAwardImage(award.ID))
            {
                award.ImageAddr = Path.Combine(Awards.ImageDirectory, award.ID.ToString());
            }
            else
            {
                award.ImageAddr = Path.Combine(Awards.ImageDirectory, Awards.DefaultImage);
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
            //if (!Awards._awardsImageList.ContainsKey(this.ID))
            //{
            //    Awards._awardsImageList.Add(this.ID, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "AwardImages", this.ID.ToString()));
            //    this.ImageAddr = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "AwardImages", this.ID.ToString());
            //}
            //else
            //{
            //    Awards._awardsImageList[this.ID] = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "AwardImages", this.ID.ToString());
            //    this.ImageAddr = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "AwardImages", this.ID.ToString());
            //}
            BusinessLogicHelper._logic.SetAwardImage(this.ID);
            //this.ImageAddr = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "AwardImages", this.ID.ToString());
            this.ImageAddr = Path.Combine(Awards.ImageDirectory, this.ID.ToString());
        }

        public void RemoveImage()
        {
            //if (Awards._awardsImageList.ContainsKey(this.ID))
            //{
            //    Awards._awardsImageList.Remove(this.ID);
            //}
            BusinessLogicHelper._logic.RemoveAwardImage(this.ID);
            //this.ImageAddr = Awards.DefaultImage;
            this.ImageAddr = Path.Combine(Users.ImageDirectory, Awards.DefaultImage);

        }

        public bool Equals(Awards award)
        {
            if ((this.ID == award.ID) && (this.Title == award.Title))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        bool IEquatable<Awards>.Equals(Awards other)
        {
            if (other == null)
            {
                return false;
            }

            if ((this.ID == other.ID) && (this.Title == other.Title))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            Awards personObj = obj as Awards;
            if (personObj == null)
                return false;
            else
                return Equals(personObj);
        }  
    }
}