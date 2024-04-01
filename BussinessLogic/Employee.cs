using System;
using Employee_Directory;
using DataInputHandler;
using DatabaseHandler;
using ConsoleTables;

namespace Employee_functions
{
    class Employee
    {
        static string[] Employeefields = Datatypes.Employeefields;
        string[] notEditableFields = Datatypes.notEditableFields;
        string[] requiredEmployeeFields = Datatypes.requiredEmployeeFields;

        string[] EmployeeFieldsFormat = Datatypes.EmployeeFieldsFormat;


        ConsoleTable table = new ConsoleTable(
                    "Sr. No.",
                    Employeefields[0],
                    Employeefields[1],
                    Employeefields[2],
                    Employeefields[3],
                    Employeefields[4],
                    Employeefields[5],
                    Employeefields[6],
                    Employeefields[7],
                    Employeefields[8],
                    Employeefields[9],
                    Employeefields[10],
                    Employeefields[11]
                );
        public void AddEmployee()
        {
            DataInput dataInput = new DataInputHandler.DataInput();
            string[] AddEmployeeData = dataInput.InputData(Employeefields, requiredEmployeeFields, EmployeeFieldsFormat);

            //display and take confirmation from user
            Console.WriteLine("\n\nEmployee Details\n");
            string[][] oneEmployeeData = new string[1][];
            oneEmployeeData[0] = AddEmployeeData;
            DisplayAsTable(oneEmployeeData);

            if (!confirmOperation())
            {
                return;
            }

            // AddEmployeeData will be added to the database
            Database db = new Database();
            db.AddEmployeeToDB(AddEmployeeData);

            Console.WriteLine("\n\nEmployee Added Successfully\n\n");

        }

        // edit employee
        public void EditEmployee(string empid)
        {
            Database db = new Database();
            // get the current data of the employee from the database
            string[] CurrentData = db.GetOneEmployeeFromDB(empid) ?? new string[12];

            DataInput dataInput = new DataInputHandler.DataInput();
            string[] EditEmployeeData = dataInput.EditData(Employeefields, CurrentData, notEditableFields, requiredEmployeeFields);


            db.EditEmployeeInDB(EditEmployeeData);
        }

        // delete employee
        public void DeleteEmployee(string empid)
        {
            // delete the employee from the database
            Database db = new Database();
            db.DeleteEmployeeFromDB(empid);
            Console.WriteLine("\n\nEmployee Deleted Successfully\n\n");
        }


        public void DisplayAsTable(string[][] tabledata)
        {
            if (tabledata.Length == 0)
            {
                Console.WriteLine("No Employees Found");
            }
            else
            {
                for (int i = 0; i < tabledata.Length; i++)
                {
                    table.AddRow(i+1, tabledata[i][0] , tabledata[i][1] , tabledata[i][2] , tabledata[i][3] , tabledata[i][4] , tabledata[i][5] , tabledata[i][6] , tabledata[i][7] , tabledata[i][8] , tabledata[i][9] , tabledata[i][10] , tabledata[i][11]);
                }

                table.Write();
            }
        }

        // display all employees
        public void DisplayAll()
        {
            // get all the employees from the database and display them
            Database db = new Database();
            string[][] AllemployeeData = db.GetAllEmployeesFromDB() ?? new string[0][];
            DisplayAsTable(AllemployeeData);
        }

        // dispaly one employee
        public void DisplayOne(string empid)
        {
            // get the employee from the database and display the employee
            Database db = new Database();
            string[][] oneEmployeeData = new string[1][];
            oneEmployeeData[0] = db.GetOneEmployeeFromDB(empid) ?? new string[0];
            DisplayAsTable(oneEmployeeData);
        }


        public bool confirmOperation()
        {
            System.Console.WriteLine("Do you want to continue? \n 1. Save Details \n 2. Cancel");
            int n = 0;
            while (true)
            {   
                System.Console.Write("Enter your choice: ");
                string? num = Console.ReadLine()?.Trim();

                if (int.TryParse(num, out n))
                {
                    if (n == 1)
                    {
                        return true;
                    }
                    else if (n == 2)
                    {
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Enter 1 or 2 only.");
                    }
                }
                else
                {
                    Console.WriteLine("Enter number only.");
                }
            }

            // This will never be reached, but it satisfies the compiler's requirement for a return at the end of the method.
            throw new InvalidOperationException("Unexpected error in confirmOperation method");
        }
    }
}

