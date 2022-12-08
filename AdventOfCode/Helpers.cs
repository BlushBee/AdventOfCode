using System;
using System.ComponentModel;
using System.IO;

namespace AdventOfCode;

public static class Helpers
{
    public static string DataPath = $"{Directory.GetCurrentDirectory()}";

    public static string[] ReadInputAsStringArrayFromDataFolder(string fileName, InputTypes inputType)
    {
        string fullPath = GetFullpathFromInputType(fileName, inputType);

        if (!File.Exists(fullPath))
        {
            CreateFileByPath(fullPath);

            return new string[0];
        }
        else
        {
            return File.ReadAllLines(fullPath);
        }
    }

    public static string ReadInputAsStringFromDataFolder(string fileName, InputTypes inputType)
    {
        string fullPath = GetFullpathFromInputType(fileName, inputType);

        if (!File.Exists(fullPath))
        {
            CreateFileByPath(fullPath);

            return string.Empty;
        }
        else
        {
            return File.ReadAllText(fullPath);
        }
    }

    private static string GetFullpathFromInputType(string fileName, InputTypes inputType)
    {
        string fullPath = string.Empty;
        switch (inputType)
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

        return fullPath;
    }

    private static void CreateFileByPath(string fullPath)
    {
        if (!File.Exists(fullPath))
        {
            using (File.Create(fullPath)) { };

            Console.WriteLine($"File not found but bas been created: {fullPath}\nNote: don't forget to paste the input data there.");
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
