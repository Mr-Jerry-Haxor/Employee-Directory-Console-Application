using System;
using Employee_Directory;
using DatabaseHandler;
using Employee_functions;

namespace DataInputHandler
{
    class DataInput
    {
        // This method takes two string arrays , one string array  contains the fileds , other string array contains the fields required, so that the user can input the data for the fields required compulsorily and the fields not required can be left empty.
        public string[] InputData(string[] fields, string[] requiredFields , string[] FieldFormats)
        {

            string[] data = new string[fields.Length];
            for (int i = 0; i < fields.Length; i++)
            {
                if (Datatypes.EmployeeLinkedToRolesFields.ContainsKey(fields[i]))
                {
                    string linkedField = Datatypes.EmployeeLinkedToRolesFields[fields[i]];

                    // get all data from the linked filed 
                    Database database = new Database(); // Create an instance of the Database class
                    string[]? fielddata = database.GetAllDataOfFieldFromRolesCollection(linkedField);// Access the method from the instance

                    Console.ForegroundColor = ConsoleColor.Yellow;

                    if (fielddata == null)
                    {
                        Console.WriteLine($"No {fields[i]} data found");
                        data[i] = "";
                        continue;
                    }
                    Console.WriteLine($"Choose {fields[i]} from the following options:");
                    if (!requiredFields.Contains(fields[i]))
                    {
                        Console.WriteLine("0. To Skip this Field");
                    }
                    for (int j = 0; j < fielddata.Length; j++)
                    {
                        Console.WriteLine($"{j + 1}. {fielddata[j]}");
                    }

                    Console.ForegroundColor = ConsoleColor.Cyan;

                    while (true)
                    {
                        Console.Write("Enter option number: ");
                        string? optionInput = Console.ReadLine();
                        if (int.TryParse(optionInput, out int optionNumber) && optionNumber >= 0 && optionNumber <= fielddata.Length)
                        {
                            if (requiredFields.Contains(fields[i]) && optionNumber == 0)
                            {
                                Console.WriteLine("Invalid option. Please try again.");
                            }
                            else if (!requiredFields.Contains(fields[i]) && optionNumber == 0)
                            {
                                data[i] = "";
                                break;
                            }
                            else
                            {
                                data[i] = fielddata[optionNumber - 1];
                                break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid option. Please try again.");
                        }
                    }

                }
                else if (requiredFields.Contains(fields[i]))
                {
                    do
                    {
                        Console.ResetColor();
                        Console.Write($"Enter {fields[i]} (eg: {FieldFormats[i]}): ");
                        string input = Console.ReadLine() ?? "";
                        input = input.Trim();

                        Console.ForegroundColor = ConsoleColor.Red;
                        if (string.IsNullOrEmpty(input))
                        {
                            Console.WriteLine(fields[i] + " (required)");
                        }
                        else if (InputValidations.Validate(input, fields[i]))
                        {
                            data[i] = input;
                            break;
                        }
                    } while (true);
                }
                else
                {
                    do
                    {
                        Console.ResetColor();

                        Console.Write($"Enter {fields[i]} (eg: {FieldFormats[i]}) (Press Enter to skip): ");
                        string input = Console.ReadLine() ?? "";
                        input = input.Trim();


                        Console.ForegroundColor = ConsoleColor.Red;

                        
                        if (string.IsNullOrEmpty(input))
                        {
                            Console.WriteLine(fields[i] + " (Skipped as not required)");
                            data[i] = "";
                            break;
                        }
                        else if (InputValidations.Validate(input, fields[i]))
                        {
                            data[i] = input;
                            break;
                        }
                    } while (true);
                }
            }
            return data;
        }
        


        public string[] EditDataByChoice(string[] fields, string[] data, string[] notEditableFields, string[] requiredFields)
        {
            string[] CurrentData = new string[data.Length];
            
            while (true)
            {

                Employee emp = new Employee();
                emp.table.AddRow("Previous Data" , data[0] , data[1], data[2] , data[3] , data[4] , data[5] , data[6] , data[7] , data[8] , data[9] , data[10] , data[11]);
                emp.table.AddRow("Updated Data" , CurrentData[0] , CurrentData[1], CurrentData[2] , CurrentData[3] , CurrentData[4] , CurrentData[5] , CurrentData[6] , CurrentData[7] , CurrentData[8] , CurrentData[9] , CurrentData[10] , CurrentData[11]);

                emp.table.Write();
                Console.WriteLine("\n\n");

                int choice = ChooseField(fields);

                if (choice.Equals(0))
                {
                    CurrentData = data;
                    break;
                }
                else if (choice.Equals(fields.Length+1))
                {
                    break;
                }
                else
                {
                    CurrentData[choice - 1] = HandleFieldChoice(fields, data, notEditableFields, requiredFields, choice);
                }
            }
            for (int i  = 0; i < data.Length;i++)
            {
                if (CurrentData[i] == null )
                {
                    CurrentData[i] = data[i];
                }
            }
            Console.WriteLine(CurrentData);
            return CurrentData;
        }

        internal string HandleFieldChoice(string[] fields, string[] data, string[] notEditableFields, string[] requiredFields ,  int choice)
        {
            int choiceindex = choice -1;
            if (notEditableFields.Contains(fields[choiceindex]))
            {
                System.Console.WriteLine("You can't Edit this Field");
                return "";
            }
            else if (Datatypes.EmployeeLinkedToRolesFields.ContainsKey(fields[choiceindex]))
            {
                string linkedField = Datatypes.EmployeeLinkedToRolesFields[fields[choiceindex]];
                System.Console.WriteLine(linkedField);
                // get all data from the linked filed 
                Database database = new Database(); // Create an instance of the Database class
                string[]? fielddata = database.GetAllDataOfFieldFromRolesCollection(linkedField); 

                Console.ForegroundColor = ConsoleColor.Yellow;
                if (fielddata == null)
                {
                    Console.WriteLine($"No {fields[choiceindex]} data found");
                    return  "";
                }
                Console.WriteLine($"Choose {fields[choiceindex]} from the following options  (Current: {data[choiceindex]}): ");
                if (!requiredFields.Contains(fields[choiceindex]))
                {
                    Console.WriteLine("0. To Skip this Field");
                }
                for (int j = 0; j < fielddata.Length; j++)
                {
                    Console.WriteLine($"{j + 1}. {fielddata[j]}");
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                while (true)
                {
                    Console.Write("Enter option number: ");
                    string? optionInput = Console.ReadLine();
                    if (int.TryParse(optionInput, out int optionNumber) && optionNumber >= 0 && optionNumber <= fielddata.Length)
                    {
                        if (requiredFields.Contains(fields[choiceindex]) && optionNumber == 0)
                        {
                            Console.WriteLine("Invalid option. Please try again.");
                        }
                        else if (!requiredFields.Contains(fields[choiceindex]) && optionNumber == 0)
                        {
                            return  "";
                        }
                        else
                        {
                            return  fielddata[optionNumber - 1];
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid option. Please try again.");
                    }
                }

            }
            else if (requiredFields.Contains(fields[choiceindex]))
            {
                do
                {
                    Console.Write($"Enter {fields[choiceindex]} (Current: {data[choiceindex]}): ");
                    string input = Console.ReadLine() ?? "";
                    input = input.Trim();

                    if (InputValidations.Validate(input, fields[choiceindex]))
                    {
                        return input;
                    }
                    else if (string.IsNullOrEmpty(input))
                    {
                        Console.WriteLine(fields[choiceindex] + " (required)");
                    }
                } while (true);
            }
            else
            {
                do
                {
                    Console.Write($"Enter {fields[choiceindex]} (Current: {data[choiceindex]}) (Press Enter to skip): ");
                    string input = Console.ReadLine() ?? "";
                    input = input.Trim();

                    if (InputValidations.Validate(input, fields[choiceindex]))
                    {
                        return  input;
                    }
                    else if (string.IsNullOrEmpty(input))
                    {
                        Console.WriteLine(fields[choiceindex] + " (Skipped as not required)");
                        return "";
                    }
                } while (true);
            }

        }

        internal int ChooseField (string[] fields)
        {
            
            System.Console.WriteLine("Edit Data : \n");
            
            for (int j = 0; j < fields.Length; j++)
            {
                Console.WriteLine($"{j + 1} : {fields[j]}");
            }
            System.Console.WriteLine("\n\n0 : Quit Editing");
            System.Console.WriteLine($"\n{fields.Length+1} : Save Editing\n");
            while (true)
            {
                Console.Write("Enter option number: ");
                string? optionInput = Console.ReadLine();
                if (int.TryParse(optionInput, out int optionNumber) && optionNumber >= 0 && optionNumber <= fields.Length+1)
                {
                    return optionNumber;
                }
                else
                {
                    Console.WriteLine("Invalid option. Please try again.");
                }
            }

        }


    }
}
