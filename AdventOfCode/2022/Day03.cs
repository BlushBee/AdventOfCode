using AoC2022.Interfaces;
using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using static AdventOfCode.Helpers;

namespace AdventOfCode;

// [MemoryDiagnoser]
[SimpleJob]
public class Day03 : ISolution
{
    private readonly InputTypes _inputTypes;
    private string[] _input;
    private Dictionary<string, string> _rucksacks = new Dictionary<string, string>();

    public Day03(InputTypes inputTypes = InputTypes.Full)
    {
        _inputTypes = inputTypes;
        ProcessInput();
    }

    [Benchmark]
    public void ProcessInput()
    {
        _input = ReadInputAsStringArrayFromDataFolder(@"2022\Input\Day03", _inputTypes);
    }

    #region first version

    // vJrwpWtwJgWrhcsFMMfFFhFp         -> vJrwpWtwJgWr     - hcsFMMfFFhFp
    // jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL -> jqHRNqRjqzjGDLGL - rsFMfFZSrLrFZsSL
    // PmmdzqPrVvPwwTWBwg               -> PmmdzqPrV        - vPwwTWBwg
    // wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn   -> 
    // ttgJtRGJQctTZtZT
    // CrZsJsPPZsGzwwsLwLmpwMDw

    [Benchmark]
    public string PartOne()
    {
        for (int i = 0; i < _input.Length; i++)
        {
            var halveInputSize = _input[i].Length / 2;
            var firstHalf = _input[i].Substring(0, halveInputSize);
            var secondHalf = _input[i].Substring(halveInputSize, halveInputSize);

            // BenchmarkDotNet complains about duplicate key while there are none so adding this check to prevent benchmark issues.
            if (!_rucksacks.ContainsKey(firstHalf))
            {
                _rucksacks.Add(firstHalf, secondHalf);
            }
        }

        var answer = 0;

        foreach (var rucksack in _rucksacks)
        {
            var firstItem = rucksack.Key.ToCharArray();
            for (int i = 0; i < firstItem.Length; i++)
            {
                if (rucksack.Value.Contains(firstItem[i]))
                {
                    answer += char.IsUpper(firstItem[i]) ? (firstItem[i]) - 38 : (firstItem[i] - 96);

                    break;
                }
            }
        }

        return answer.ToString();
    }

    [Benchmark]
    public string PartTwo()
    {
        var answer = 0;
        for (int i = 0; i < _input.Length; i+=3)
        {
            var firstGroup = _input[i].ToCharArray();
            var secondGroup = _input[i+1].ToCharArray();
            var thirdGroup = _input[i+2].ToCharArray();

            for(int j = 0; j < firstGroup.Length; j++)
            {
                if (secondGroup.Contains(firstGroup[j]) && thirdGroup.Contains(firstGroup[j]))
                {
                    answer += char.IsUpper(firstGroup[j]) ? (firstGroup[j]) - 38 : (firstGroup[j] - 96);
                    break;
                }
            }             
        }

        return answer.ToString();

    }

    #endregion
}
