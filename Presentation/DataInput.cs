using System;
using Employee_Directory;
using DatabaseHandler;

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
        


        // this method takes four string array as input, one string array contains the fields and the second  string array contains the data , third string array contains the fields not editable, so that the user can edit the fields which are not in the third string array. fourth string array is the required fields.
        public string[] EditData(string[] fields, string[] data, string[] notEditableFields, string[] requiredFields)
        {
            for (int i = 0; i < fields.Length; i++)
            {
                if (notEditableFields != null && !notEditableFields.Contains(fields[i]))
                {
                    if (Datatypes.EmployeeLinkedToRolesFields.ContainsKey(fields[i]))
                    {
                        string linkedField = Datatypes.EmployeeLinkedToRolesFields[fields[i]];
                        System.Console.WriteLine(linkedField);
                        // get all data from the linked filed 
                        Database database = new Database(); // Create an instance of the Database class
                        string[]? fielddata = database.GetAllDataOfFieldFromRolesCollection(linkedField); 

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        if (fielddata == null)
                        {
                            Console.WriteLine($"No {fields[i]} data found");
                            data[i] = "";
                            continue;
                        }
                        Console.WriteLine($"Choose {fields[i]} from the following options  (Current: {data[i]}): ");
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
                            Console.Write($"Enter {fields[i]} (Current: {data[i]}): ");
                            string input = Console.ReadLine() ?? "";
                            input = input.Trim();

                            if (InputValidations.Validate(input, fields[i]))
                            {
                                data[i] = input;
                                break;
                            }
                            else if (string.IsNullOrEmpty(input))
                            {
                                Console.WriteLine(fields[i] + " (required)");
                            }
                        } while (true);
                    }
                    else
                    {
                        do
                        {
                            Console.Write($"Enter {fields[i]} (Current: {data[i]}) (Press Enter to skip): ");
                            string input = Console.ReadLine() ?? "";
                            input = input.Trim();

                            if (InputValidations.Validate(input, fields[i]))
                            {
                                data[i] = input;
                                break;
                            }
                            else if (string.IsNullOrEmpty(input))
                            {
                                Console.WriteLine(fields[i] + " (Skipped as not required)");
                                data[i] = "";
                                break;
                            }
                        } while (true);
                    }
                }
            }
            return data;
        }

    }
}
