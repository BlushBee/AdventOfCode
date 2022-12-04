using AoC2022.Interfaces;
using BenchmarkDotNet.Attributes;
using System;
using static AoC2022.Helpers;

namespace AoC2022
{
    // [MemoryDiagnoser]
    // [SimpleJob]
    internal class Template : ISolution
    {
        private readonly InputTypes _inputTypes;
        private string[] _input;

        public Template(InputTypes inputTypes = InputTypes.Full)
        {
            _inputTypes = inputTypes;
            ProcessInput();
        }

        [Benchmark]
        public void ProcessInput()
        {
            _input = ReadInputFromDataFolder("DayX", _inputTypes);
        }

        [Benchmark]
        public int PartOne()
        {
            throw new NotImplementedException();
        }

        [Benchmark]
        public int PartTwo()
        {
            throw new NotImplementedException();
        }
    }
}
