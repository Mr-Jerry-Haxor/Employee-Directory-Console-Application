using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using Employee_Directory;
using MongoDB.Bson.Serialization.Serializers;

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
                    document.Add(new BsonElement(Employeefields[i], AddEmployeeData[i]));
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
            // try
            // {
            //     var database = connect();
            //     var collection = database.GetCollection<BsonDocument>("employees");

            //     var filter = Builders<BsonDocument>.Filter.Eq(Employeefields[0], EditEmployeeData[0]);

            //     var updateDefinition = Builders<BsonDocument>.Update.Empty;
            //     for (int i = 1; i < Employeefields.Length; i++)
            //     {
            //         updateDefinition = updateDefinition.Set(Employeefields[i], EditEmployeeData[i]);
            //     }

            //     collection.UpdateOne(filter, updateDefinition);
            //     Console.WriteLine("Employee edited successfully");
            // }
            // catch (Exception ex)
            // {
            //     Console.WriteLine("Error while editing employee :" + ex.Message);
            //     throw;
            // }
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
                        employee[i] = documents[j].GetElement(Employeefields[i]).Value?.ToString() ?? string.Empty;
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
                        employeeData[i] = document.GetElement(Employeefields[i]).Value?.ToString() ?? string.Empty;
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

        public class Roles
        {
            public string? RoleName { get; set; }
            public string? Department { get; set; }
            public string? RoleDescription { get; set; }
            public string? Location { get; set; }
        }
        // add role to the database
        internal void AddRoleToDB(string[] AddRoledata)
        {
            
            try
            {
                var database = connect();
                var collection = database.GetCollection<BsonDocument>("roles");

                var role = new BsonDocument();
                for (int i = 0; i < Rolefields.Length; i++)
                {
                    role.Add(new BsonElement(Rolefields[i], AddRoledata[i]));
                }

                collection.InsertOne(role);
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
                    role[0] = documents[j].GetElement("RoleName").Value?.ToString() ?? string.Empty;
                    role[1] = documents[j].GetElement("Department").Value?.ToString() ?? string.Empty;
                    role[2] = documents[j].GetElement("RoleDescription").Value?.ToString() ?? string.Empty;
                    role[3] = documents[j].GetElement("Location").Value?.ToString() ?? string.Empty;
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