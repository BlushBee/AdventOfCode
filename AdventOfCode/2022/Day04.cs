using AoC2022.Interfaces;
using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using static AdventOfCode.Helpers;

namespace AdventOfCode;

// [MemoryDiagnoser]
[SimpleJob]
public class Day04 : ISolution
{
    private readonly InputTypes _inputTypes;
    private string[] _input;

    public Day04(InputTypes inputTypes = InputTypes.Full)
    {
        _inputTypes = inputTypes;
        ProcessInput();
    }

    [Benchmark]
    public void ProcessInput()
    {
        _input = ReadInputFromDataFolder(@"2022\Input\Day04", _inputTypes);
    }

    #region first version

    [Benchmark]
    public int PartOne()
    {
        var total = 0;
        for (int i = 0; i < _input.Length; i++)
        {
            var ids = _input[i].Split(',');
            var firstPair = ids[0].Split('-');
            var secondPair = ids[1].Split('-');

            if ((int.Parse(firstPair[0]) >= int.Parse(secondPair[0]) && int.Parse(firstPair[1]) <= int.Parse(secondPair[1])) || (int.Parse(firstPair[0]) <= int.Parse(secondPair[0]) && int.Parse(firstPair[1]) >= int.Parse(secondPair[1])))
            {
                total++;
            }
        }

        return total;

    }

    // In the above example, the first two pairs(2-4,6-8 and 2-3,4-5) don't overlap, while the remaining four pairs (5-7,7-9, 2-8,3-7, 6-6,4-6, and 2-6,4-8) do overlap:

    // 5-7,7-9 overlaps in a single section, 7.
    // 2-8,3-7 overlaps all of the sections 3 through 7.
    // 6-6,4-6 overlaps in a single section, 6.
    // 2-6,4-8 overlaps in sections 4, 5, and 6.

    [Benchmark]
    public int PartTwo()
    {
        var total = 0;
        for (int i = 0; i < _input.Length; i++)
        {
            var ids = _input[i].Split(',');
            var firstPair = ids[0].Split('-');
            var secondPair = ids[1].Split('-');
            var hasOverlap = false;

            for (int j = int.Parse(firstPair[0]); j <= int.Parse(firstPair[1]); j++)
            {
                for (int k = int.Parse(secondPair[0]); k <= int.Parse(secondPair[1]); k++)
                {
                    if (!hasOverlap && j == k)
                    {
                        hasOverlap = true;
                        total++;
                        break;
                    }
                }

                if (hasOverlap)
                {
                    break;
                }
            }

            hasOverlap = false;
        }

        return total;
    }

    #endregion
}
