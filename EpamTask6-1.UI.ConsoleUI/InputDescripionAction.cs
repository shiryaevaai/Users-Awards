using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using EpamTask6_1.UserList.Entities;
using EpamTask6_1.UserList.Logic;

namespace EpamTask6_1.UI.ConsoleUI
{
    public class InputDescripionAction
    {
        private string input;

        private string description;

        private Action doAction;

        public string Input
        {
            get
            {
                return this.input;
            }

            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.input = value;
                }
                else
                {
                    throw new ArgumentException("Строка не может быть пустой!");
                }
            }
        }

        public string Description
        {
            get
            {
                return this.description;
            }

            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.description = value;
                }
                else
                {
                    throw new ArgumentException("Строка не может быть пустой!");
                }
            }
        }

        public Action DoAction
        {
            get
            {
                return this.doAction;
            }

            set
            {
                this.doAction = value; // какая проверка?
            }
        }

        public InputDescripionAction(string input, string description, Action action)
        {
            this.Input = input;
            this.Description = description;
            this.DoAction = action;

        }
    }
}
