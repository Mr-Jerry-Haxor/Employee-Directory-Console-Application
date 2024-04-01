using System;
using System.Text.RegularExpressions;
using DatabaseHandler;

namespace Employee_Directory
{
    class InputValidations
    {

        // employee fileds { "EmployeeId", "FirstName", "LastName", "DateOfBirth", "Email", "Mobile",
        //  "JoiningDate", "Location", "JobTitle", "Department", "Manager", "Project" };
        public static bool Validate(string input, string fieldname)
        {
            if (fieldname == "EmployeeId")
            {
                string pattern = @"^TZ\d{4}$";
                if (!Regex.IsMatch(input, pattern))
                {
                    Console.WriteLine("Employee Id should start with 'TZ' and followed by 4 digits");
                    return false;
                }
                else if (new Database().CheckEmployeeExists(input))
                {
                    Console.WriteLine("Employee Id already exists");
                    return false;
                }
            }
            else if (fieldname == "RoleId" )
            {
                string pattern = @"^R\d{4}$";
                if (!Regex.IsMatch(input, pattern))
                {
                    Console.WriteLine("Role Id should start with 'R' and followed by 4 digits");
                    return false;
                }
                else if (new Database().CheckRoleExists(input))
                {
                    Console.WriteLine("Role Id already exists");
                    return false;
                }

            }
            else if (fieldname == "FirstName" || fieldname == "LastName" || fieldname == "Location" || fieldname == "JobTitle" || fieldname == "Department" || fieldname == "RoleName" || fieldname == "RoleDescription" )
            {
                if (input.Length < 2)
                {
                    Console.WriteLine(fieldname + " should be of minimum 2 characters");
                    return false;
                }
                if (!Regex.IsMatch(input, @"^[a-zA-Z\s]+$"))
                {
                    Console.WriteLine(fieldname + " should start with only alphabets");
                    return false;
                }
            }
            else if (fieldname == "DateOfBirth" || fieldname == "JoiningDate")
            {
                string pattern = @"^\d{2}-\d{2}-\d{4}$";
                if (!Regex.IsMatch(input, pattern))
                {
                    Console.WriteLine(fieldname + " should be in the format dd-mm-yyyy");
                    return false;
                }
                //checking the date is valid date
                else if (!DateTime.TryParseExact(input, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out _))
                {
                    Console.WriteLine("Invalid Date");
                    return false;
                }
                else 
                {
                    // check if the age is greater than 18
                    DateTime dob = DateTime.ParseExact(input, "dd-MM-yyyy", null);
                    DateTime today = DateTime.Today;
                    int age = today.Year - dob.Year;
                    if (dob > today.AddYears(-age))
                    {
                        age--;
                    }
                    if (fieldname == "DateOfBirth" && age < 18 || age > 100)
                    {
                        Console.WriteLine("Age should be greater than 18 and less than 100 years");
                        return false;
                    }
                    else if (fieldname == "JoiningDate" && age >1 || age < 0)
                    {
                        Console.WriteLine("Joining Date should be less than 1 year from current date");
                        return false;
                    }
                }
            }
            else if (fieldname == "Email")
            {
                string pattern = @"^[^\d]\S+@\S+\.\S+$";
                if (!Regex.IsMatch(input, pattern))
                {
                    Console.WriteLine("Email should be in the format example@example.com");
                    return false;
                }
            }
            else if (fieldname == "Mobile")
            {
                string pattern = @"^\d{10}$";
                if (!Regex.IsMatch(input, pattern))
                {
                    Console.WriteLine("Mobile number should be of 10 digits");
                    return false;
                }
            }
            

            return true;
        }
    }
}