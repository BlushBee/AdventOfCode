using System;
using System.ComponentModel;
using System.IO;

namespace AdventOfCode;

public static class Helpers
{
    public static string DataPath = $"{Directory.GetCurrentDirectory()}";

    public static string[] ReadInputFromDataFolder(string fileName, InputTypes inputTypes)
    {
        string fullPath = string.Empty;

        switch (inputTypes)
        {
            case InputTypes.Full:
                fullPath = $"{DataPath}\\{fileName} Input.txt";
                break;
            case InputTypes.Example:
                fullPath = $"{DataPath}\\{fileName} ExampleInput.txt";
                break;
            case InputTypes.Alternative:
                fullPath = $"{DataPath}\\{fileName} AlternativeInput.txt";
                break;
            default:
                break;
        }

        if (!File.Exists(fullPath))
        {
            using (File.Create(fullPath)){};

            Console.WriteLine($"File not found but bas been created: {fullPath}\nNote: don't forget to paste the input data there.");

            return new string[0];
        }
        else
        {
            return File.ReadAllLines(fullPath);
        }
    }

    public enum InputTypes
    {
        [Description("Example input from AoC")]
        Example,
        [Description("Full input from AoC")]
        Full,
        [Description("Alternative input")]
        Alternative
    }  
}
