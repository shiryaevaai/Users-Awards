namespace EpamTask6_1.UI.ConsoleUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    using EpamTask6_1.UserList.Entities;
    using EpamTask6_1.UserList.Logic;

    public class ConsoleMenu
    {
        private List<InputDescripionAction> _actionsList;

        private string commandFormat = "Команда: {0,-10} Описание: {1}";

        private string exit = "exit";

        private string printMenu = "menu";

        public ConsoleMenu(List<InputDescripionAction> _inputList)
        {
            if (_inputList.Any(i => i.Input == "exit"))
            {
                throw new ArgumentException("Команда \"exit\" зарезервирована");
            }

            if (_inputList.Any(i => i.Input == "menu"))
            {
                throw new ArgumentException("Команда \"menu\" зарезервирована");
            }
            
            if (_inputList.GroupBy(i=>i.Input).Any(g => g.Count() > 1))
            {
                throw new ArgumentException("Нельзя задать две одинаковых команды");
            }

            this._actionsList = _inputList;  
        }

        public void PrintMenu()
        {
            foreach (var item in this._actionsList)
            {
                Console.WriteLine(this.commandFormat, item.Input, item.Description);
            }

            Console.WriteLine(this.commandFormat, this.printMenu, "Просмотр меню");
            Console.WriteLine(this.commandFormat, this.exit, "Выход из подпрограммы");
        }

        public void DoAction()
        {
            this.PrintMenu();

            while (true)
            {
                Console.WriteLine();
                string input = Console.ReadLine();

                try
                {
                    string description = this._actionsList.FirstOrDefault(n => n.Input == input).Description;
                    Action newAction = this._actionsList.FirstOrDefault(n => n.Input == input).DoAction;

                    try
                    {
                        Console.WriteLine(description);
                        newAction.Invoke();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                catch 
                {
                    if (input == "exit")
                    {
                        return;
                    }
                    else if (input == "menu")
                    {
                        this.PrintMenu();
                    }
                    else
                    {
                        throw new ArgumentException("Введена неверная команда");
                    }
                }                  

                Console.WriteLine();
            }
        }
    }
}
