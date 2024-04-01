
using System.Collections.Generic; 

namespace Employee_Directory
{
    // create inferance of the datatype
    public  class Datatypes
    {
        public static string[] menufields = new string[] { "MainMenu", "EmployeeMenu", "RoleMenu" };
        public static string[] EmployeeMenufields = new string[] { "Add Employee", "Display All", "Display One", "Edit Employee", "Delete Employee", "Go Back" };
        public static string[] RoleMenufields = new string[] { "Add Role", "Display all", "Go Back" };


        public static string[] Employeefields = new string[] { "EmployeeId", "FirstName", "LastName", "DateOfBirth", "Email", "Mobile", "JoiningDate", "Location", "JobTitle", "Department", "Manager", "Project" };
        public static string[] notEditableFields = new string[] { "EmployeeId" };
        public static string[] requiredEmployeeFields = new string[] { "EmployeeId", "FirstName", "LastName", "Email", "JoiningDate", "Location", "JobTitle", "Department" };
        public static string[] EmployeeFieldsFormat = new string[] { "TZ0000", "John", "John", "dd-mm-yyyy", "example@email.com", "987654321", "dd-mm-yyyy", "HYD", "Software Developer", "PE", "", "" };

        public static Dictionary<string, string> EmployeeLinkedToRolesFields = new Dictionary<string, string>
        {
            { "JobTitle", "RoleName" },
            { "Department", "RoleDepartment" }
        };


        public static string[] Rolefields = new string[] { "RoleId", "RoleName", "RoleDepartment" , "RoleDescription"  , "Location"};
        public static string[] notEditableRoleFields = new string[] { "RoleId" };
        public static string[] requiredRoleFields = new string[] { "RoleId", "RoleName", "RoleDepartment" , "RoleDescription" };
        public static string[] RoleFieldsFormat = new string[] { "R0000", "Software Developer", "PE", "Develops software", "HYD" };
    }

} 


