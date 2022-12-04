using BenchmarkDotNet.Running;
using System;
using static AoC2022.Helpers;

namespace AoC2022
{
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
            var day1 = new Day1(inputType);
            Console.WriteLine($"Day 1 Part #1: {day1.PartOne()}");
            Console.WriteLine($"Day 1 Part #2: {day1.PartTwo()}");
            Console.WriteLine("");

            var day2 = new Day2(inputType);
            Console.WriteLine($"Day 2 Part #1: {day2.PartOne()}");
            Console.WriteLine($"Day 2 Part #2: {day2.PartTwo()}");
            Console.WriteLine($"Day 2 Part #1: {day2.PartOneOptimized()} (optimized)");
            Console.WriteLine($"Day 2 Part #2: {day2.PartTwoOptimized()} (optimized)");
            Console.WriteLine("");
        }

        private static void RunBenchmarks()
        {
            // var result = BenchmarkRunner.Run(typeof(Program).Assembly);

            //_ = BenchmarkRunner.Run<Day1>();
            _ = BenchmarkRunner.Run<Day2>();
        }
    }
}
