using AoC2022.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static AoC2022.Helpers;

namespace AoC2022
{
    internal class Day1 : ISolution
    {
        private readonly InputTypes _inputTypes;
        private string[] _input;
        private Dictionary<int, int> _elvesWithTotalCalories = new Dictionary<int, int>();

        public Day1(InputTypes inputType)
        {
            _inputTypes = inputType;
            ProcessInput();
        }

        public void ProcessInput()
        {
            _input = ReadInputFromDataFolder("Day1", _inputTypes);
            int total = 0;           

            for (int i = 0; i < _input.Length; i++)
            {
                if (!string.IsNullOrEmpty(_input[i]))
                {
                    _ = int.TryParse(_input[i], out int calories);
                    total += calories;
                }
                else
                {
                    _elvesWithTotalCalories.Add(_elvesWithTotalCalories.Count + 1, total);
                    total = 0;
                }
            }
        }

        public void PartOne()
        {
            var topX = 1;
            var topXCalories = GetTopElvesWithCalories(_elvesWithTotalCalories, topX);
            var highestAmountOfCalories = GetTotalCalories(topXCalories);

            Console.WriteLine($"Day 1 Part #1: {highestAmountOfCalories}");
        }

        public void PartTwo()
        {
            var topX = 3;
            var topXCalories = GetTopElvesWithCalories(_elvesWithTotalCalories, topX);
            var highestAmountOfCalories = GetTotalCalories(topXCalories);

            Console.WriteLine($"Day 1 Part #2: {highestAmountOfCalories}");
        }

        private int GetTotalCalories(Dictionary<int, int> groups)
        {
            return groups.Sum(v => v.Value);
        }

        private Dictionary<int, int> GetTopElvesWithCalories(Dictionary<int, int> groups, int top)
        {
            return groups.OrderByDescending(o => o.Value).Take(top).ToDictionary(k => k.Key, v => v.Value);
        }
    }
}
