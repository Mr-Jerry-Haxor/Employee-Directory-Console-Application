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
            string[] AddEmployeeData = dataInput.InputData(Employeefields, requiredEmployeeFields);

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

        // display all employees
        public void DisplayAll()
        {
            // get all the employees from the database and display them
            Database db = new Database();
            string[][] AllemployeeData = db.GetAllEmployeesFromDB() ?? new string[0][];
            if (AllemployeeData.Length == 0)
            {
                Console.WriteLine("No Employees Found");
            }
            else
            {
                for (int i = 0; i < AllemployeeData.Length; i++)
                {
                    table.AddRow(i+1, AllemployeeData[i][0] , AllemployeeData[i][1] , AllemployeeData[i][2] , AllemployeeData[i][3] , AllemployeeData[i][4] , AllemployeeData[i][5] , AllemployeeData[i][6] , AllemployeeData[i][7] , AllemployeeData[i][8] , AllemployeeData[i][9] , AllemployeeData[i][10] , AllemployeeData[i][11]);
                }

                table.Write();
            }
        }

        // dispaly one employee
        public void DisplayOne(string empid)
        {
            // get the employee from the database and display the employee
            Database db = new Database();
            string[] oneEmployeeData = db.GetOneEmployeeFromDB(empid) ?? new string[0];

            if (oneEmployeeData.Length == 0)
            {
                Console.WriteLine("Employee not found");
            }
            else
            {
                table.AddRow("1" , oneEmployeeData[0] , oneEmployeeData[1] , oneEmployeeData[2] , oneEmployeeData[3] , oneEmployeeData[4] , oneEmployeeData[5] , oneEmployeeData[6] , oneEmployeeData[7] , oneEmployeeData[8] , oneEmployeeData[9] , oneEmployeeData[10] , oneEmployeeData[11]);
                table.Write();
            }
        }

    }
}