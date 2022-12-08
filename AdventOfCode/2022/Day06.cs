using AoC2022.Interfaces;
using BenchmarkDotNet.Attributes;
using System.Linq;
using static AdventOfCode.Helpers;

namespace AdventOfCode;

// [MemoryDiagnoser]
[SimpleJob]
public class Day06 : ISolution
{
    private readonly InputTypes _inputTypes;
    private string _input;

    public Day06(InputTypes inputTypes = InputTypes.Full)
    {
        _inputTypes = inputTypes;
        ProcessInput();
    }

    [Benchmark]
    public void ProcessInput()
    {
        _input = ReadInputAsStringFromDataFolder(@"2022\Input\Day06", _inputTypes);
    }

    #region first version

    [Benchmark]
    public string PartOne()
    {
        var amountOfUniqueChars = 4;
        var answer = GetAmountOfCharactersAfterFirstMarker(amountOfUniqueChars);

        return answer;
    }

    [Benchmark]
    public string PartTwo()
    {
        var amountOfUniqueChars = 14;
        var answer = GetAmountOfCharactersAfterFirstMarker(amountOfUniqueChars);

        return answer;
    }

    public string GetAmountOfCharactersAfterFirstMarker(int amountOfUniqueChars)
    {
        var currentCharacters = new string[amountOfUniqueChars];

        for (int i = 0; i < _input.Length; i++)
        {
            for (int j = 0; j < amountOfUniqueChars; j++)
            {
                currentCharacters[j] = _input.Substring(i + j, 1);
            }

            var uniqueCharacters = currentCharacters.Distinct().Count();

            if (uniqueCharacters == amountOfUniqueChars)
            {
                return (i + amountOfUniqueChars).ToString();
            }
        }

        return "N/A";
    }

    #endregion


    #region Optimized version

    // when atempting to write an optimized version

    #endregion
}
