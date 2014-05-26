using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpamTask6_1.UserList.Entities
{
    // create its own dao
    public class Award
    {
        private string title;
        public Guid ID { get; set; }

        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                if (value != "")
                {
                    this.title = value;
                }
                else
                {
                    throw new ArgumentException("Вводимая строка не может быть пустой");//
                }
            }
        }

        public Award()
        {
        }

        public Award(string title)
        {
            this.ID = Guid.NewGuid();
            this.Title = title;
        }
    }
}
