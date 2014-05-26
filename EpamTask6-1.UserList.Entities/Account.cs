namespace EpamTask6_1.UserList.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Account
    {
        public Guid ID { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public List<Role> RoleList = new List<Role>();
    }
}
