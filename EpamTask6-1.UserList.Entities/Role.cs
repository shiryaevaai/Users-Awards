namespace EpamTask6_1.UserList.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;

    public class Role
    {
        public Guid ID { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Длина строки должна быть от 1 до 255 символов")]
        public string RoleName { get; set; }
    }
}
