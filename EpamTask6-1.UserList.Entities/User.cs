namespace EpamTask6_1.UserList.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class User
    {
        private string name;
        private DateTime dateOfBirth;
        private List<Guid> _awardList = new List<Guid>();
        public Guid ID { get; set; }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                if (value != "")
                {
                    this.name = value;
                }
                else
                {
                    throw new ArgumentException("Вводимая строка не может быть пустой");//
                }
            }
        }

        public DateTime DateOfBirth
        {
            get
            {
                return this.dateOfBirth;
            }

            set
            {
                if (DateTime.Now < value)
                {
                    throw new ArgumentException("Неверный ввод даты рождения"); 
                }
                else
                if (DateTime.Now.Year - value.Year > 150 )
                {
                    throw new ArgumentException("Неверный ввод даты рождения");
                }
                else
                {
                    this.dateOfBirth = value;
                }
            }
        }

        public int Age 
        { 
            get
            {
                int days, months, years;
                if (DateTime.Now < this.DateOfBirth)
                {
                    throw new ArgumentException("Неверный ввод даты рождения");//
                }
                else
                {
                    years = DateTime.Now.Year - this.DateOfBirth.Year;
                    months = DateTime.Now.Month - this.DateOfBirth.Month;
                    if (months < 0)
                    {
                        months += 12;
                        years--;
                    }

                    days = DateTime.Now.Day - this.DateOfBirth.Day;
                    if (days < 0)
                    {
                        days += DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                        months--;
                        if (months < 0)
                        {
                            months += 12;
                            years--;
                        }

                        if (months >= 12)
                        {
                            months -= 12;
                            years++;
                        }
                    }

                    if ((days < 0) || (months < 0) || (years < 0))
                    {
                        throw new ArgumentException("Неверный ввод даты!");//
                    }

                    if (years > 150)
                    {
                        throw new ArgumentException("Слишком большой возраст");//
                    }
                }

                return years;
            }
        }

        public void AddAward(Guid id)
        {
            this._awardList.Add(id);
        }

        public List<Guid> GetAwardList()
        {
            return this._awardList;     // ??
        }

        public User(string name, DateTime dateOfBirth)
        {
            this.ID = Guid.NewGuid();
            this.Name = name;
            this.DateOfBirth = dateOfBirth;
        }

        public User(string name, DateTime dateOfBirth, IEnumerable<Guid> inputList) :
            this(name, dateOfBirth)
        {
            this.ID = Guid.NewGuid();
            this.Name = name;
            this.DateOfBirth = dateOfBirth;
            this._awardList = inputList.ToList();
        }
    }
}
