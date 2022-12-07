using AoC2022.Interfaces;
using BenchmarkDotNet.Attributes;
using System.Collections.Generic;
using System.Linq;
using static AdventOfCode.Helpers;

namespace AdventOfCode;

//[MemoryDiagnoser]
[SimpleJob]
public class Day01 : ISolution
{
    private readonly InputTypes _inputTypes;
    private string[] _input;
    private Dictionary<int, int> _elvesWithTotalCalories = new Dictionary<int, int>();

    public Day01(InputTypes inputType = InputTypes.Full)
    {
        _inputTypes = inputType;
        ProcessInput();
    }

    [Benchmark]
    public void ProcessInput()
    {
        _input = ReadInputFromDataFolder(@"2022\Input\Day01", _inputTypes);
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

    #region first version

    [Benchmark]
    public string PartOne()
    {
        var topX = 1;
        var topXCalories = GetTopElvesWithCalories(_elvesWithTotalCalories, topX);
        var answer = GetTotalCalories(topXCalories);

        return answer.ToString();
    }

    [Benchmark]
    public string PartTwo()
    {
        var topX = 3;
        var topXCalories = GetTopElvesWithCalories(_elvesWithTotalCalories, topX);
        var answer = GetTotalCalories(topXCalories);

        return answer.ToString();
    }



    private int GetTotalCalories(Dictionary<int, int> groups)
    {
        return groups.Sum(v => v.Value);
    }

    private Dictionary<int, int> GetTopElvesWithCalories(Dictionary<int, int> groups, int top)
    {
        return groups.OrderByDescending(o => o.Value).Take(top).ToDictionary(k => k.Key, v => v.Value);
    }

    #endregion
}
