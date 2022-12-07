using AoC2022.Interfaces;
using BenchmarkDotNet.Attributes;
using System;
using static AdventOfCode.Helpers;

namespace AdventOfCode;

// [MemoryDiagnoser]
[SimpleJob]
public class Template : ISolution
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
        _input = ReadInputFromDataFolder(@"2022\Input\Day01", _inputTypes);
    }

    #region first version

    [Benchmark]
    public string PartOne()
    {
        throw new NotImplementedException();
    }

    [Benchmark]
    public string PartTwo()
    {
        throw new NotImplementedException();
    }

    #endregion


    #region Optimized version

    // when atempting to write an optimized version

    #endregion
}
