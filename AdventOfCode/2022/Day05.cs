using AoC2022.Interfaces;
using BenchmarkDotNet.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AdventOfCode.Helpers;

namespace AdventOfCode;


// Todo: rewrite PartOne() & PartTwo() to remove duplicate code.

// [MemoryDiagnoser]
[SimpleJob]
public class Day05 : ISolution
{
    private readonly InputTypes _inputTypes;
    private string[] _input;

    public Day05(InputTypes inputTypes = InputTypes.Full)
    {
        _inputTypes = inputTypes;
        ProcessInput();
    }

    [Benchmark]
    public void ProcessInput()
    {
        _input = ReadInputFromDataFolder(@"2022\Input\Day05", _inputTypes);
    }

    #region first version

    [Benchmark]
    public string PartOne()
    {
        var instructionsStartAtLine = 0; // stack + instructions are in one file and we only want to start moving when the instructions begin
        var amountOfStacks = 0;
        var stacksOfCargo = new List<Stack<char>>();

        // get initial stack of cargo
        for (int i = 0; i < _input.Length; i++)
        {
            if (!string.IsNullOrEmpty(_input[i]))
            {
                instructionsStartAtLine++;
            }
            else
            {
                amountOfStacks = int.Parse(_input[instructionsStartAtLine - 1].Where(w => char.IsNumber(w)).Max().ToString());
                instructionsStartAtLine = i;
                break;
            }
        }

        // create empty stacks first depending on amount of stacks required
        for (int i = 0; i < amountOfStacks; i++)
        {
            stacksOfCargo.Add(new Stack<char>());
        }

        // add cargo to stacks
        for (int i = instructionsStartAtLine - 2; i >= 0; i--)
        {
            var crates = _input[i].ToCharArray();

            for (int j = 0; j < crates.Length; j += 4)
            {
                var crateNumber = (j / 4);
                if (crates[j + 1] != ' ')
                {
                    stacksOfCargo[crateNumber].Push(crates[j + 1]);
                }
            }
        }

        // move cargo from stack
        for (int i = instructionsStartAtLine + 1; i < _input.Length; i++)
        {
            var instructionSplit = _input[i].Split(' ');
            var instruction = (amountToMove: int.Parse(instructionSplit[1]), from: int.Parse(instructionSplit[3]) - 1, to: int.Parse(instructionSplit[5]) - 1);

            while (instruction.amountToMove > 0)
            {
                stacksOfCargo[instruction.to].Push(stacksOfCargo[instruction.from].Pop());
                instruction.amountToMove--;
            }
        }

        // check what character is on top of each stack
        var answer = new StringBuilder();

        for (int i = 0; i < stacksOfCargo.Count; i++)
        {
            answer.Append(stacksOfCargo[i].Peek());
        }

        return answer.ToString();
    }

    [Benchmark]
    public string PartTwo()
    {
        var instructionsStartAtLine = 0; // stack + instructions are in one file and we only want to start moving when the instructions begin
        var amountOfStacks = 0;
        var stacksOfCargo = new List<Stack<char>>();

        // get initial stack of cargo
        for (int i = 0; i < _input.Length; i++)
        {
            if (!string.IsNullOrEmpty(_input[i]))
            {
                instructionsStartAtLine++;
            }
            else
            {
                amountOfStacks = int.Parse(_input[instructionsStartAtLine - 1].Where(w => char.IsNumber(w)).Max().ToString());
                instructionsStartAtLine = i;
                break;
            }
        }

        // create empty stacks first depending on amount of stacks required
        for (int i = 0; i < amountOfStacks; i++)
        {
            stacksOfCargo.Add(new Stack<char>());
        }

        // add cargo to stacks
        for (int i = instructionsStartAtLine - 2; i >= 0; i--)
        {
            var crates = _input[i].ToCharArray();

            for (int j = 0; j < crates.Length; j += 4)
            {
                var crateNumber = (j / 4);
                if (crates[j + 1] != ' ')
                {
                    stacksOfCargo[crateNumber].Push(crates[j + 1]);
                }
            }
        }

        // move cargo from stack
        for (int i = instructionsStartAtLine + 1; i < _input.Length; i++)
        {
            var instructionSplit = _input[i].Split(' ');
            var instruction = (amountToMove: int.Parse(instructionSplit[1]), from: int.Parse(instructionSplit[3]) - 1, to: int.Parse(instructionSplit[5]) - 1);

            if (instruction.amountToMove == 1)
            {
                stacksOfCargo[instruction.to].Push(stacksOfCargo[instruction.from].Pop());
            }
            else
            {
                while (instruction.amountToMove > 0)
                {
                    for (int j = 0; j < instruction.amountToMove; j++)
                    {
                        var multipleCargo = stacksOfCargo[instruction.from].Take(instruction.amountToMove).ToList();

                        for (int k = multipleCargo.Count() - 1; k >= 0; k--)
                        {
                            stacksOfCargo[instruction.to].Push(multipleCargo[k]);
                            stacksOfCargo[instruction.from].Pop();
                            instruction.amountToMove--;
                        }
                    }
                }
            }
        }

        // check what character is on top of each stack
        var answer = new StringBuilder();

        for (int i = 0; i < stacksOfCargo.Count; i++)
        {
            answer.Append(stacksOfCargo[i].Peek());
        }

        return answer.ToString();
    }

    #endregion


    #region Optimized version

    // when atempting to write an optimized version

    #endregion
}
