////Задание 1 
////Реализовать приложение с консольным интерфейсом, позволяющее работать со списком пользователей 
////(User: Id, Name, DateOfBirth, Age): создавать, просматривать и удалять их. В качестве архитектурного 
////шаблона применить трёхслойную архитектуру. Записи должны сохраняться в файл, а во время работы 
////приложения они должны кэшироваться для повышения производительности. 
namespace EpamTask6_1.UI.ConsoleUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using EpamTask6_1.UserList.Entities;
    using EpamTask6_1.UserList.Logic;

    public class Program
    {
        public static UserListLogic _logic;    

        static Program()
        {
            _logic = new UserListLogic();
        }

        //public delegate void Action();

        static private event Action CreateUserEvent;

        static private event Action GetAllUsersEvent;

        static private event Action DeleteUserEvent;

        static private event Action CreateAwardEvent;

        static private event Action GetAllAwardsEvent;

        static private event Action GetUserAwardsEvent;

        static private event Action AddAwardToUserEvent;

        public static void CallAction()
        {
            if (CreateUserEvent != null)
            {
                CreateUser();
                CreateUserEvent = null;
            }

            if (GetAllUsersEvent != null)
            {
                GetAllUsers();
                GetAllUsersEvent = null;
            }

            if (DeleteUserEvent != null)
            {
                DeleteUser();
                DeleteUserEvent = null;
            }

            if (CreateAwardEvent != null)
            {
                CreateAward();
                CreateAwardEvent = null;
            }

            if (GetAllAwardsEvent != null)
            {
                GetAllAwards();
                GetAllAwardsEvent = null;
            }

            if (GetUserAwardsEvent != null)
            {
                GetUserAwards();

                GetUserAwardsEvent = null;
            }

            if (AddAwardToUserEvent != null)
            {
                AddAwardToUser();

                AddAwardToUserEvent = null;
            }
        }

        internal static void CreateUser()
        {
            Console.WriteLine("Добавление нового пользователя");
            Console.WriteLine("Введите имя пользователя: ");
            var name = Console.ReadLine();
            if (name == " " || name == "")
            {
                Console.WriteLine("Имя не может быть пустым!"); // // (InvalidCastExeption)
                return;
            }

            Console.Write("Введите дату рождения пользователя в формате dd.mm.yyyy: ");
            DateTime birthDate;
            string[] inputDate;

            try
            {
                inputDate = Console.ReadLine().Split('.');
                birthDate = new DateTime(int.Parse(inputDate[2]), int.Parse(inputDate[1]), int.Parse(inputDate[0]));
            }
            catch
            {
                Console.WriteLine("Неверный ввод даты!"); // // (InvalidCastExeption)
                return;
            }

            try
            {
                if (_logic.AddUser(name, birthDate))
                {
                    Console.WriteLine("Пользователь успешно добавлен.");
                }
                else
                {
                    Console.WriteLine("Добавления не произошло.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Добавления не произошло.");
                Console.WriteLine(ex.Message);
            }
        }

        internal static void GetAllUsers()
        {
            Console.WriteLine("Информация о пользователях ");            
            List<User> _userInfo = _logic.GetAllUsers().ToList<User>();
            if (_userInfo.Count == 0)
            {
                Console.WriteLine("В системе нет пользователей ");
            }
            else
            {
                Console.WriteLine("{0,-36} {1,-20} {2,-20} {3,-5}", "ID", "Имя", "Дата рождения", "Возраст");
                foreach (var item in _userInfo)
                {
                    Console.WriteLine(
                        "{0,-36} {1,-20} {2,-20:dd.MM.yyyy} {3,-5}", 
                        item.ID.ToString(), 
                        item.Name.ToString(),
                        item.DateOfBirth, 
                        item.Age.ToString());
                }
            }
        }

        internal static void DeleteUser()
        {
            // Delete all connected awards?+
            Console.WriteLine("Удаление пользователя");
            Console.WriteLine("Введите ID пользователя: ");
            string input = Console.ReadLine();
            Guid newGuid;
            if (!Guid.TryParse(input, out newGuid))
            {
                Console.WriteLine("Неверно введен ID пользователя! ");
                return;
            }

            try
            {
                User newUser = _logic.GetUserByID(newGuid);

                if (_logic.DeleteUser(newUser))
                {
                    Console.WriteLine("Пользователь с ID={0} успешно удален.", input);
                }
                else
                {
                    Console.WriteLine("Удаления не произошло.");
                }
            }
            catch
            {
                Console.WriteLine("Неверно введен ID пользователя! ");
                return;
            }
        }

        internal static void CreateAward()
        {
            Console.WriteLine("Добавление нового вида награды");
            Console.WriteLine("Введите название награды: ");
            var title = Console.ReadLine();

            try
            {
                if (_logic.AddAward(title))
                {
                    Console.WriteLine("Награда успешно добавлена.");
                }
                else
                {
                    Console.WriteLine("Добавления не произошло.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Добавления не произошло.");
                Console.WriteLine(ex.Message);
            }
        }

        internal static void GetAllAwards()
        {
            Console.WriteLine("Информация о наградах ");
            List<Award> _awardInfo = _logic.GetAllAwards().ToList<Award>();
            if (_awardInfo.Count == 0)
            {
                Console.WriteLine("В системе нет наград ");
            }
            else
            {
                Console.WriteLine("{0,-36} {1,-20} ", "ID", "Название награды");
                foreach (var item in _awardInfo)
                {
                    Console.WriteLine("{0,-36} {1,-20} ", item.ID.ToString(), item.Title.ToString());
                }
            }
        }

        internal static void GetUserAwards()
        {
            Console.WriteLine("Информация о наградах ");
            Console.WriteLine("Введите ID пользователя ");
            string input = Console.ReadLine();
            Guid newGuid;
            if (!Guid.TryParse(input, out newGuid))
            {
                Console.WriteLine("Неверно введен ID пользователя! ");
                return;
            }

            try
            {
                User newUser = _logic.GetUserByID(newGuid);
                Award[] userAwards = _logic.GetUserAwards(newUser);

                if (userAwards.Length == 0)
                {
                    Console.WriteLine("У этого пользователя пока что нет наград ");
                }
                else
                {
                    Console.WriteLine("{0,-36} {1,-10} ", "ID", "Имя");
                    Console.WriteLine("{0,-36} {1,-10} ", newUser.ID.ToString(), newUser.Name.ToString());

                    Console.WriteLine("Список наград пользователя: ");
                    Console.WriteLine("{0,-36} {1,-20} ", "ID", "Название награды");

                    foreach (var item in userAwards)
                    {
                        Console.WriteLine("{0,-36} {1,-20} ", item.ID.ToString(), item.Title.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Неверно введен ID пользователя! ");
                Console.WriteLine(ex.Message);
                return;
            }
        }

        internal static void AddAwardToUser()
        {
            Console.WriteLine("Наградить пользователя "); 
            Console.WriteLine("Введите ID пользователя ");
            string input = Console.ReadLine();
            Guid userID;
            if (!Guid.TryParse(input, out userID))
            {
                Console.WriteLine("Неверно введен ID пользователя! ");
                return;
            }

            Console.WriteLine("Введите ID награды ");
            input = Console.ReadLine();
            Guid awardID;
            if (!Guid.TryParse(input, out awardID))
            {
                Console.WriteLine("Неверно введен ID награды! ");
                return;
            }

            try
            {
                if (_logic.AddAwardToUser(userID, awardID))
                {
                    Console.WriteLine("Награда успешно добавлена.");
                }
                else
                {
                    Console.WriteLine("Добавления не произошло.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Добавления не произошло.");
                Console.WriteLine(ex.Message);
                return;
            }
        }

        internal static void Main(string[] args)
        {
            _logic.StartCashing();
            Console.WriteLine("Наберите \'1\', чтобы создать пользователя");
            Console.WriteLine("Наберите \'2\', чтобы просмотреть список пользователей");
            Console.WriteLine("Наберите \'3\', чтобы удалить пользователя");
            Console.WriteLine("Наберите \'4\', чтобы добавить вид награды");
            Console.WriteLine("Наберите \'5\', чтобы добавить награду пользователя");
            Console.WriteLine("Наберите \'6\', чтобы просмотреть виды наград");
            Console.WriteLine("Наберите \'7\', чтобы просмотреть награды пользователя");
                  Console.WriteLine("Нажмите \'q\' для выхода из программы.");
            Console.WriteLine();

            while (true)
            {
                switch (Console.ReadLine())
                {
                    case "1":
                        {
                            CreateUserEvent += new Action(Program.CreateUser);
                            break;
                        }

                    case "2":
                        {
                            GetAllUsersEvent += new Action(Program.GetAllUsers);
                            break;
                        }

                    case "3":
                        {
                            DeleteUserEvent += new Action(Program.DeleteUser);
                            break;
                        }

                    case "4":
                        {
                            CreateAwardEvent += new Action(Program.CreateAward);
                            break;
                        }                                                
     
                    case "5":
                        {
                            AddAwardToUserEvent += new Action(Program.AddAwardToUser);
                            break;
                        }

                    case "6":
                        {
                            GetAllAwardsEvent += new Action(Program.GetAllAwards);
                            break;
                        }

                    case "7":
                        {
                            GetUserAwardsEvent += new Action(Program.GetUserAwards);
                            break;
                        }

                    case "q":
                        {
                            _logic.FinalCashing();
                            return;
                        }

                    default:
                        {
                            break;
                        }
                }

                CallAction();
                Console.WriteLine();
            }
        }
    }
}

/* Как хранить награды и пользователей:
 * 1. сделать общий файл  ид записи - ид пользователя - ид награды. тогда список наград для каждого пользователя извлекать из этого файла
 2. сохранять пользователя как ид - имя - дата создания -награда1 - награда2... минус - все время перезаписывать пользователей
 */