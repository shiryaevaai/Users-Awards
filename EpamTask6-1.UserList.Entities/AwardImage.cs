namespace EpamTask6_1.UserList.Entities
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AwardImage
    {
        private static string awardImageDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "AwardImages");

        private static string defaultAwardImage = "default.jpg";

       // private static string defaultAwardImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "AwardImages", "default.jpg");

        private string image;

        public Guid AwardID { get; set; }

        public static string DefaultAwardImage 
        {
            get
            {
                return defaultAwardImage;
            }

            set
            {
                defaultAwardImage = value;

            }
        }

        public static string AwardImageDirectory
        {
            get
            {
                return awardImageDirectory;
            }

            private set
            {
                awardImageDirectory = value;

            }
        }

        public string Image { get; set; }

        public AwardImage(Guid id, string address)
        {
            AwardID = id;
            Image = address;
            
        }
    }
}
