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

        public static Dictionary<int, Guid> UserIDs = new Dictionary<int, Guid>(20);

        public static Dictionary<int, Guid> AwardIDs = new Dictionary<int, Guid>(20);

        public static string getUserFormatString = "{0,-3} {1,-20} {2,-20} {3,-5}";
        public static string getUserShortFormatString = "{0,-3} {1,-10} ";
        public static string getAwardFormatString = "{0,-3} {1,-20} ";
        
        public static void FillUserIDs()
        {
            UserIDs.Clear();
            List<User> _userInfo = _logic.GetAllUsers().ToList<User>();
            int i = 1;
            foreach (var item in _userInfo)
            {
                UserIDs.Add(i, item.ID);
                i++;
            }
        }

        public static void FillAwardIDs()
        {
            AwardIDs.Clear();
            List<Award> _awardInfo = _logic.GetAllAwards().ToList<Award>();
            int i = 1;
            foreach (var item in _awardInfo)
            {
                AwardIDs.Add(i, item.ID);
                i++;
            }
        }

        static Program()
        {
            try
            {
                _logic = new UserListLogic();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                Environment.Exit(0);
            }
        }

        internal static void CreateUser()
        {
           // Console.WriteLine("Добавление нового пользователя");
            Console.WriteLine("Введите имя пользователя: ");
            var name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Имя не может быть пустым!"); // // (InvalidCastExeption)
                return;
            }

            Console.Write("Введите дату рождения пользователя в формате dd.mm.yyyy: ");
            DateTime birthDate;
            string[] inputDate;

            try
            {
                inputDate = Console.ReadLine().Split(new[]{'.'}, 3);
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
         //   Console.WriteLine("Информация о пользователях ");
            List<User> _userInfo = _logic.GetAllUsers().ToList<User>();
            if (_userInfo.Count == 0)
            {
                Console.WriteLine("В системе нет пользователей ");
            }
            else
            {
                Console.WriteLine(getUserFormatString, "ID", "Имя", "Дата рождения", "Возраст");
                FillUserIDs();
                foreach (var item in UserIDs)
                {
                    User user = _userInfo.Single(n => n.ID == UserIDs[item.Key]);
                    Console.WriteLine(
                        getUserFormatString,
                        item.Key.ToString(),
                        user.Name,
                        user.DateOfBirth.ToString("dd.MM.yyyy"),
                        user.Age.ToString());
                }

                //foreach (var item in _userInfo)
                //{
                //    Console.WriteLine(
                //        getUserFormatString,
                //        UserIDs[item.ID].ToString(),
                //        item.Name.ToString(),
                //        item.DateOfBirth.ToString("dd.MM.yyyy"),
                //        item.Age.ToString());
                //}
            }
        }

        internal static void DeleteUser()
        {
          //  Console.WriteLine("Удаление пользователя");
            Console.WriteLine("Введите ID пользователя: ");
            string input = Console.ReadLine();

            if (!UserIDs.ContainsKey(int.Parse(input)))
            {
                Console.WriteLine("Неверно введен ID пользователя! ");
                return;
            }

            Guid newGuid = UserIDs[int.Parse(input)];

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
         //   Console.WriteLine("Добавление нового вида награды");
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
         //   Console.WriteLine("Информация о наградах ");
            List<Award> _awardInfo = _logic.GetAllAwards().ToList<Award>();
            if (_awardInfo.Count == 0)
            {
                Console.WriteLine("В системе нет наград ");
            }
            else
            {
                FillAwardIDs();
                Console.WriteLine(getAwardFormatString, "ID", "Название награды");
                //foreach (var item in _awardInfo)
                //{
                //    Console.WriteLine(getAwardFormatString, AwardIDs[item.ID].ToString(), item.Title.ToString());
                //}
                                    
                foreach (var item in AwardIDs)
                {
                    Award award = _awardInfo.Single(n => n.ID == AwardIDs[item.Key]);
                    Console.WriteLine(
                        getAwardFormatString,
                        item.Key.ToString(),
                        award.Title.ToString());
                        
                }
            }
        }

        internal static void GetUserAwards()
        {
          //  Console.WriteLine("Информация о наградах ");
            Console.WriteLine("Введите ID пользователя ");
            string input = Console.ReadLine();

            if (!UserIDs.ContainsKey(int.Parse(input)))
            {
                Console.WriteLine("Неверно введен ID пользователя! ");
                return;
            }

            Guid newGuid = UserIDs[int.Parse(input)];

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
                    Console.WriteLine(getUserShortFormatString, "ID", "Имя");
                    Console.WriteLine(getUserShortFormatString, input, newUser.Name.ToString());

                    Console.WriteLine("Список наград пользователя: ");
                    Console.WriteLine(getAwardFormatString, "ID", "Название награды");

                    foreach (var award in userAwards)
                    {
                        Console.WriteLine(
                            getAwardFormatString, 
                            AwardIDs.Single(n=>n.Value==award.ID).Key.ToString(), 
                            award.Title.ToString());
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
         //   Console.WriteLine("Наградить пользователя "); 
            Console.WriteLine("Введите ID пользователя ");
            string input = Console.ReadLine();

            if (!UserIDs.ContainsKey(int.Parse(input)))
            {
                Console.WriteLine("Неверно введен ID пользователя! ");
                return;
            }

            Guid userID = UserIDs[int.Parse(input)];

            Console.WriteLine("Введите ID награды ");
            input = Console.ReadLine();
            if (!AwardIDs.ContainsKey(int.Parse(input)))
            {
                Console.WriteLine("Неверно введен ID награды! ");
                return;
            }

            Guid awardID = AwardIDs[int.Parse(input)];

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
            List<InputDescripionAction> _inputList = new List<InputDescripionAction>(8);

            try
            {
                _inputList.Add(new InputDescripionAction("1", "Создание пользователя", CreateUser));
                _inputList.Add(new InputDescripionAction("2", "Просмотр списка пользователей", GetAllUsers));
                _inputList.Add(new InputDescripionAction("3", "Удаление пользователя", DeleteUser));
                _inputList.Add(new InputDescripionAction("4", "Добавление нового вида награды", CreateAward));
                _inputList.Add(new InputDescripionAction("5", "Награждение пользователя", AddAwardToUser));
                _inputList.Add(new InputDescripionAction("6", "Просмотр видов наград", GetAllAwards));
                _inputList.Add(new InputDescripionAction("7", "Просмотр наград пользователя", GetUserAwards));
               // _inputList.Add(new InputDescripionAction("", "Просмотр наград пользователя", GetUserAwards));

                ConsoleMenu menu = new ConsoleMenu(_inputList);
                menu.DoAction();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }           
                
            Console.ReadLine();
        }
    }
}

/* Как хранить награды и пользователей:
 * 1. сделать общий файл  ид записи - ид пользователя - ид награды. тогда список наград для каждого пользователя извлекать из этого файла
 2. сохранять пользователя как ид - имя - дата создания -награда1 - награда2... минус - все время перезаписывать пользователей
 */