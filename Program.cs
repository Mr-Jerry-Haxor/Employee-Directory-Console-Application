using Menu_List;
using HandleUserChoice;

namespace EmployeeDirectory
{
    static class Globals
    {
        public static string MainMenu = "MainMenu";
        public static string EmployeeMenu = "EmployeeMenu";
        public static string RoleMenu = "RoleMenu";

    }
    class Program
    {
        public static void Main()
        {
            Console.WriteLine("Hello , Welcome to Employee Directory");

            string userstate = Globals.MainMenu;
            Menu menu = new Menu();
            HandleChoice handlechoice = new HandleChoice();


            while (true)
            {
                int n = 0;

                Console.ForegroundColor = ConsoleColor.Cyan;
                //load menu and takes the user input
                menu.LoadMenu(userstate);

                n = menu.UserMenuInput();

                Console.ResetColor();

                //check the user state and change the user state

                userstate = handlechoice.CheckStateAndChange(userstate, n);


            }

        }
    }

}


