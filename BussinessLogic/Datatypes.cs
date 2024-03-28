

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
        public static string[] Rolefields = new string[] { "RoleId", "RoleName", "Department" , "RoleDescription"  , "Location"};
        public static string[] notEditableRoleFields = new string[] { "RoleId" };
        public static string[] requiredRoleFields = new string[] { "RoleId", "RoleName", "Department" , "RoleDescription" };
    }
} 


