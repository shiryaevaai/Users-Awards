namespace EpamTask6_1.UserList.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Account
    {
        public Guid ID { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Длина строки должна быть от 1 до 255 символов")]
        public string Login { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Длина строки должна быть от 1 до 255 символов")]
        public string Password { get; set; }

        public List<Role> RoleList = new List<Role>();
    }
}
