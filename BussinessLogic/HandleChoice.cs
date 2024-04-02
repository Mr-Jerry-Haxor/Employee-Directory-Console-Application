using Employee_functions;
using EmployeeDirectory;
using Role_functions;
using DatabaseHandler;

namespace HandleUserChoice
{

    class HandleChoice
    {
        public string CheckStateAndChange(string userstate, int n)
        {

            if (userstate.Equals(Globals.MainMenu.ToString()))
            {
                switch (n)
                {
                    case 1:
                        userstate = Globals.EmployeeMenu.ToString();
                        break;
                    case 2:
                        userstate = Globals.RoleMenu.ToString();
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                    default:
                        System.Console.WriteLine("Invalid Choice, Please choose from available numbers only.");
                        break;
                }
            }
            else if (userstate.Equals(Globals.EmployeeMenu.ToString()))
            {
                Employee employee = new Employee();

                switch (n)
                {
                    case 1:
                        Console.WriteLine("Add Employee");
                        employee.AddEmployee();
                        break;
                    case 2:
                        Console.WriteLine(" \n--------------------------------------------------\nDisplay All");
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        employee.DisplayAll();
                        Console.ResetColor();
                        break;
                    case 3:
                        Console.WriteLine("\n--------------------------------------------------\nDisplay One");
                        string Displayempid = UserInputForEmpID();
                        if (Displayempid != "/q")
                        {
                            employee.DisplayOne(Displayempid);
                        }
                        else
                        {
                            userstate = Globals.EmployeeMenu.ToString();
                        }
                        break;
                    case 4:
                        Console.WriteLine("Edit Employee");
                        string Editempid = UserInputForEmpID();
                        if (Editempid != "/q")
                        {
                            employee.EditEmployee(Editempid);
                        }
                        else
                        {
                            userstate = Globals.EmployeeMenu.ToString();
                        }
                        break;
                    case 5:
                        Console.WriteLine("Delete Employee");
                        string DeleteEmpid = UserInputForEmpID();
                        if (DeleteEmpid != "/q")
                        {
                            employee.DeleteEmployee(DeleteEmpid);
                        }
                        else
                        {
                            userstate = Globals.EmployeeMenu.ToString();
                        }
                        break;
                    case 6:
                        userstate = Globals.MainMenu.ToString();
                        break;
                    default:
                        userstate = Globals.EmployeeMenu.ToString();
                        System.Console.WriteLine("Invalid Choice, Please choose from available numbers only.");
                        break;
                }
            }
            else if (userstate.Equals(Globals.RoleMenu.ToString()))
            {

                Role role = new Role();

                switch (n)
                {
                    case 1:
                        Console.WriteLine("Add Role");
                        role.AddRole();
                        break;
                    case 2:
                        Console.WriteLine("\n--------------------------------------------------\nDisplay All");
                        role.DisplayAll();
                        break;
                    case 3:
                        userstate = Globals.MainMenu.ToString();
                        break;
                    default:
                        userstate = Globals.RoleMenu.ToString();
                        System.Console.WriteLine("Invalid Choice, Please choose from available numbers only.");
                        break;
                }
            }
            return userstate;
        }


        string UserInputForEmpID()
        {

            Console.Write("Enter Employee Id:( Enter /q to quit operation ) ");
            string? empid = Console.ReadLine();
            while (string.IsNullOrEmpty(empid))
            {
                Console.WriteLine("Employee Id is required");
                Console.Write("Enter Employee Id: ");
                empid = Console.ReadLine();
            }
            Database db = new Database();

            if (empid.Equals("/q"))
            {
                return "/q";
            }

            if (db.CheckEmployeeExists(empid))
            {
                return empid;
            }
            else
            {
                Console.WriteLine("Employee not found");
                return "/q";
            }
        }
    }
}