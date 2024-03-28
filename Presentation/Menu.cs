using System;
using Microsoft.VisualBasic;

namespace Menu_List
{
    class Menu
    {

        string MainMenuString = "\n 1 . Employee Management\n 2 . Role Management\n 3 . Exit";

        string EmployeeMenuString = "\n 1 . Add Employee\n 2 . Display All\n 3 . Dsiplay One\n 4 . Edit Employee\n 5 . Delete Employee\n 6 . Go Back ";

        string RoleMenuString = "\n 1 . Add Role\n 2 . Display all\n 3 . Go Back";
        string SelectChoiceString = " Enter index to choose: ";
        public void LoadMenu(string userstate)
        {
            if (userstate.Equals("MainMenu"))
            {
                Console.WriteLine("\nMain Menu");

                Console.WriteLine(MainMenuString);
                Console.Write(SelectChoiceString);
            }
            else if (userstate.Equals("EmployeeMenu"))
            {
                Console.WriteLine("\nEmployee Management Menu");

                Console.WriteLine(EmployeeMenuString);
                Console.Write(SelectChoiceString);
            }
            else if (userstate.Equals("RoleMenu"))
            {
                Console.WriteLine("\nRole Management Menu");

                Console.WriteLine(RoleMenuString);
                Console.Write(SelectChoiceString);
            }
        }

        public int UserMenuInput()
        {
            int n = 0;

            bool success = true;
            while (success)
            {
                string? num = Console.ReadLine()?.Trim();

                if (int.TryParse(num, out n))
                {
                    success = false;
                }
                else
                {
                    Console.Write("Enter number only: ");
                }
            }

            return n;
        }
    }
}
