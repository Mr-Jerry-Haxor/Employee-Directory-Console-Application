using System;
using Employee_Directory;

namespace DataInputHandler
{
    class DataInput
    {
        // This method takes two string arrays , one string array  contains the fileds , other string array contains the fields required, so that the user can input the data for the fields required compulsorily and the fields not required can be left empty.
        public string[] InputData(string[] fields, string[] requiredFields)
        {

            string[] data = new string[fields.Length];
            for (int i = 0; i < fields.Length; i++)
            {
                if (requiredFields.Contains(fields[i]))
                {
                    do
                    {
                        Console.ResetColor();
                        Console.Write($"Enter {fields[i]}: ");
                        string input = Console.ReadLine() ?? "";
                        input = input.Trim();

                        Console.ForegroundColor = ConsoleColor.Red;
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
                        Console.ResetColor();
                        
                        Console.Write($"Enter {fields[i]} (Press Enter to skip): ");
                        string input = Console.ReadLine() ?? "";
                        input = input.Trim();


                        Console.ForegroundColor = ConsoleColor.Red;

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
            return data;
        }
        


        // this method takes four string array as input, one string array contains the fields and the second  string array contains the data , third string array contains the fields not editable, so that the user can edit the fields which are not in the third string array. fourth string array is the required fields.
        public string[] EditData(string[] fields, string[] data, string[] notEditableFields, string[] requiredFields)
        {
            for (int i = 0; i < fields.Length; i++)
            {
                if (!notEditableFields.Contains(fields[i]))
                {
                    if (requiredFields.Contains(fields[i]))
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