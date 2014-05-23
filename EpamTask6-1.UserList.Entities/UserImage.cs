namespace EpamTask6_1.UserList.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class UserImage
    {
        private static string defaultUserImage;

        private string userImage;

        public Guid UserID { get; set; }

        public static string DefaultUserImage { get; set; }

        public string UserImage { get; set; }
    }
}
