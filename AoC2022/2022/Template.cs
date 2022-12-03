using AoC2022.Interfaces;
using System;
using static AoC2022.Helpers;

namespace AoC2022
{
    internal class Template : ISolution
    {
        private readonly InputTypes _inputTypes;
        private string[] _input;

        public Template(InputTypes inputTypes)
        {
            _inputTypes = inputTypes;
            ProcessInput();
        }

        public void ProcessInput()
        {
            _input = ReadInputFromDataFolder("DayX", _inputTypes);

            throw new NotImplementedException();            
        }

        public void PartOne()
        {
            Console.WriteLine($"Day x Part #1:");

            throw new NotImplementedException();
        }

        public void PartTwo()
        {
            Console.WriteLine($"Day x Part #2:");

            throw new NotImplementedException();
        }
    }
}
