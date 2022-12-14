using BenchmarkDotNet.Running;
using System;
using static AdventOfCode.Helpers;

namespace AdventOfCode;

internal static class Program
{
    static void Main(string[] args)
    {
        var inputType = InputTypes.Full;
        var runBenchmarks = true;

        Console.WriteLine($"Solutions running on input: {inputType}");
        ShowAnswers(inputType);

        #if RELEASE

            if (runBenchmarks)
            {
                RunBenchmarks();
            }

        #endif

        Console.ReadKey();
    }

    private static void ShowAnswers(InputTypes inputType)
    {
        var day01 = new Day01(inputType);
        Console.WriteLine($"Day 1 Part #1: {day01.PartOne()}");
        Console.WriteLine($"Day 1 Part #2: {day01.PartTwo()}");
        Console.WriteLine("");

        var day02 = new Day02(inputType);
        Console.WriteLine($"Day 2 Part #1: {day02.PartOne()}");
        Console.WriteLine($"Day 2 Part #2: {day02.PartTwo()}");
        Console.WriteLine($"Day 2 Part #1: {day02.PartOneOptimized()} (optimized)");
        Console.WriteLine($"Day 2 Part #2: {day02.PartTwoOptimized()} (optimized)");
        Console.WriteLine("");

        var day03 = new Day03(inputType);
        Console.WriteLine($"Day 3 Part #1: {day03.PartOne()}");
        Console.WriteLine($"Day 3 Part #2: {day03.PartTwo()}");
        Console.WriteLine("");

        var day04 = new Day04(inputType);
        Console.WriteLine($"Day 4 Part #1: {day04.PartOne()}");
        Console.WriteLine($"Day 4 Part #2: {day04.PartTwo()}");
        Console.WriteLine($"Day 4 Part #2: {day04.PartTwoOptimized()} (optimized)");
        Console.WriteLine("");

        var day05 = new Day05(inputType);
        Console.WriteLine($"Day 5 Part #1: {day05.PartOne()}");
        Console.WriteLine($"Day 5 Part #2: {day05.PartTwo()}"); 
        Console.WriteLine("");

        var day06 = new Day06(inputType);
        Console.WriteLine($"Day 6 Part #1: {day06.PartOne()}");
        Console.WriteLine($"Day 6 Part #2: {day06.PartTwo()}");
        Console.WriteLine("");
    }

    private static void RunBenchmarks()
    {
        // var result = BenchmarkRunner.Run(typeof(Program).Assembly);

        // _ = BenchmarkRunner.Run<Day01>();
        // _ = BenchmarkRunner.Run<Day02>();
        // _ = BenchmarkRunner.Run<Day03>();
        // _ = BenchmarkRunner.Run<Day04>();
        _ = BenchmarkRunner.Run<Day05>();
    }
}
