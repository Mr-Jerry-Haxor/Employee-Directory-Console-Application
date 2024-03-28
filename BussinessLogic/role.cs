using DataInputHandler;
using DatabaseHandler;
using ConsoleTables;
using Employee_Directory;

namespace Role_functions
{
    class Role
    {
        string[] RoleFields = Datatypes.Rolefields;
        string[] ROleRequiredFields = Datatypes.requiredRoleFields;
        // add role
        public void AddRole()
        {
            DataInput dataInput = new DataInputHandler.DataInput();
            string[] AddRoledata = dataInput.InputData(RoleFields, ROleRequiredFields);
            // AddRoledata will be added to the database
            Database db = new Database();
            db.AddRoleToDB(AddRoledata);
        }

        // display all roles
        public void DisplayAll()
        {
            // get all the roles from the database and display them
            Database db = new Database();
            string[][] roles =  db.GetAllRolesFromDB() ?? new string[0][];
            ConsoleTable table = new ConsoleTable("Sr. No.", RoleFields[0], RoleFields[1], RoleFields[2], RoleFields[3], RoleFields[4]);
            for (int i = 0; i < roles.Length; i++)
            {
                table.AddRow(i + 1, roles[i][0], roles[i][1], roles[i][2], roles[i][3] , roles[i][4]);
            }
            table.Write();
        }
    }
}