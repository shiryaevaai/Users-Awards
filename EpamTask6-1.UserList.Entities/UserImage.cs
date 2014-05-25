namespace EpamTask6_1.UserList.Entities
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class UserImage
    {
        private static string userImageDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "UserImages");

        private static string defaultUserImage = "default.jpg";

        private string image;

        public Guid UserID { get; set; }

        public static string DefaultUserImage 
        {
            get
            {
                return defaultUserImage;
            }

            private set
            {
                defaultUserImage = value;

            }
        }

        public static string UserImageDirectory
        {
            get
            {
                return userImageDirectory;
            }

            private set
            {
                userImageDirectory = value;

            }
        }

        public string Image { get; set; }

        public UserImage(Guid id, string address)
        {
            UserID = id;
            Image = address;
            
        }
    }
}
