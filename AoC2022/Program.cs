using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static AoC2022.Helpers;

namespace AoC2022
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var useExampleInput = InputTypes.Alternative;

            Console.WriteLine($"Solutions running on input: {useExampleInput}\n");

            var day1 = new Day1(useExampleInput);
            day1.PartOne();
            day1.PartTwo();

            Console.WriteLine("");

            var day2 = new Day2(useExampleInput);
            day2.PartOne();
            day2.PartTwo();

            Console.WriteLine("");

            Console.ReadKey();
        }
    }
}
