using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using Employee_Directory;

namespace DatabaseHandler1
{
    class Database
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();


        string[] Rolefields = Datatypes.Rolefields;
        string[] Employeefields = Datatypes.Employeefields;

        //constructor
        public IMongoDatabase connect()
        {
            var connectionString = config.GetSection("MongoSettings:ConnectionString").Value;
            var databaseName = config.GetSection("MongoSettings:DatabaseName").Value;

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            return database;
        }

        // add employee  to the database
        internal void AddEmployeeToDB(string[] AddEmployeeData)
        {
            try
            {
                var database = connect();
                var collection = database.GetCollection<BsonDocument>("employees");

                var document = new BsonDocument();
                for (int i = 0; i < Employeefields.Length; i++)
                {
                    document.Add(Employeefields[i], AddEmployeeData[i]);
                }

                collection.InsertOne(document);
                Console.WriteLine("Employee added successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while adding employee :" + ex.Message);
                throw;
            }
        }

        // edit employee in the database
        internal void EditEmployeeInDB(string[] EditEmployeeData)
        {

            try
            {
                var database = connect();
                var collection = database.GetCollection<BsonDocument>("employees");

                var filter = Builders<BsonDocument>.Filter.Eq(Employeefields[0], EditEmployeeData[0]);

                var update = Builders<BsonDocument>.Update
                    .Set(Employeefields[1], EditEmployeeData[1])
                    .Set(Employeefields[2], EditEmployeeData[2])
                    .Set(Employeefields[3], EditEmployeeData[3])
                    .Set(Employeefields[4], EditEmployeeData[4])
                    .Set(Employeefields[5], EditEmployeeData[5])
                    .Set(Employeefields[6], EditEmployeeData[6])
                    .Set(Employeefields[7], EditEmployeeData[7])
                    .Set(Employeefields[8], EditEmployeeData[8])
                    .Set(Employeefields[9], EditEmployeeData[9])
                    .Set(Employeefields[10], EditEmployeeData[10])
                    .Set(Employeefields[11], EditEmployeeData[11]);

                collection.UpdateOne(filter, update);
                Console.WriteLine("Employee edited successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while editing employee :" + ex.Message);
                throw;
            }
        }

        // delete employee from the database
        internal void DeleteEmployeeFromDB(string empid)
        {
            try
            {
                var database = connect();
                var collection = database.GetCollection<BsonDocument>("employees");

                var filter = Builders<BsonDocument>.Filter.Eq(Employeefields[0], empid);

                collection.DeleteOne(filter);
                Console.WriteLine("Employee deleted successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while deleting employee :" + ex.Message);
                throw;
            }
        }

        // get all employees from the database
        internal string[][]? GetAllEmployeesFromDB()
        {
            try
            {
                var database = connect();
                var collection = database.GetCollection<BsonDocument>("employees");

                var documents = collection.Find(new BsonDocument()).ToList();

                string[][] employees = new string[documents.Count][];
                for (int j = 0; j < documents.Count; j++)
                {
                    string[] employee = new string[12];
                    for (int i = 0; i < Employeefields.Length; i++)
                    {
                        employee[i] = documents[j][Employeefields[i]]?.ToString() ?? string.Empty;
                    }
                    employees[j] = employee;
                }
                return employees;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while getting all employees :" + ex.Message);
                throw;
            }
        }

        // get one employee from the database
        internal string[]? GetOneEmployeeFromDB(string empid)
        {
            try
            {
                var database = connect();
                var collection = database.GetCollection<BsonDocument>("employees");
                var filter = Builders<BsonDocument>.Filter.Eq("EmployeeId", empid);
                var document = collection.Find(filter).FirstOrDefault();

                // return string array of the employee data
                if (document != null)
                {
                    string[] employeeData = new string[12];
                    for (int i = 0; i < Employeefields.Length; i++)
                    {
                        employeeData[i] = document[Employeefields[i]]?.ToString() ?? string.Empty;
                    }
                    return employeeData;
                }
                else
                {
                    Console.WriteLine("Employee not found");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while getting one employee :" + ex.Message);
                throw;
            }
        }

        // add role to the database

        internal void AddRoleToDB(string[] AddRoledata)
        {
            try
            {
                var database = connect();
                var collection = database.GetCollection<BsonDocument>("roles");

                var document = new BsonDocument
                {

                    {"RoleId", AddRoledata[0]},
                    {"RoleName", AddRoledata[1]},
                    {"Department", AddRoledata[2]},
                    {"RoleDescription", AddRoledata[3]},
                    {"Location", AddRoledata[4]}
                };
                collection.InsertOne(document);
                Console.WriteLine("Role added successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while adding role :" + ex.Message);
                throw;
            }
        }

        // get all roles from the database
        internal string[][]? GetAllRolesFromDB()
        {
            try
            {
                var database = connect();
                var collection = database.GetCollection<BsonDocument>("roles");

                var documents = collection.Find(new BsonDocument()).ToList();

                string[][] roles = new string[documents.Count][];
                for (int j = 0; j < documents.Count; j++)
                {
                    string[] role = new string[4];
                    role[0] = documents[j]["RoleId"]?.ToString() ?? string.Empty;
                    role[1] = documents[j]["RoleName"]?.ToString() ?? string.Empty;
                    role[2] = documents[j]["Department"]?.ToString() ?? string.Empty;
                    role[3] = documents[j]["RoleDescription"]?.ToString() ?? string.Empty;
                    role[4] = documents[j]["Location"]?.ToString() ?? string.Empty;
                    roles[j] = role;
                }

                return roles;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while getting all roles :" + ex.Message);
                throw;
            }
        }


        //check id empid exists or not

        internal bool CheckEmployeeExists(string empid)
        {
            try
            {
                var database = connect();
                var collection = database.GetCollection<BsonDocument>("employees");
                var filter = Builders<BsonDocument>.Filter.Eq("EmployeeId", empid);
                var document = collection.Find(filter).FirstOrDefault();
                if (document != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while checking employee exists :" + ex.Message);
                throw;
            }
        }


    }
}

