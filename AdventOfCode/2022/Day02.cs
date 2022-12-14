using AoC2022.Interfaces;
using BenchmarkDotNet.Attributes;
using System.Collections.Generic;
using static AdventOfCode.Helpers;

namespace AdventOfCode;

// [MemoryDiagnoser]
[SimpleJob]
public class Day02 : ISolution
{
    private readonly InputTypes _inputTypes;
    private string[] _input;

    public Day02(InputTypes inputTypes = InputTypes.Full)
    {
        _inputTypes = inputTypes;
        ProcessInput();
    }

    [Benchmark]
    public void ProcessInput()
    {
        _input = ReadInputAsStringArrayFromDataFolder(@"2022\Input\Day02", _inputTypes);
    }

    #region first version

    [Benchmark]
    public string PartOne()
    {
        var score = 0;
        for (int i = 0; i < _input.Length; i++)
        {
            var choiceInRound = _input[i].Split(' ');
            var opponentPick = GetHandpickToEnum(choiceInRound[0]);
            var playerPick = GetHandpickToEnum(choiceInRound[1]);
            score += GetRoundScore(opponentPick, playerPick, GameType.Normal);
        }

        return score.ToString();
    }

    [Benchmark]
    public string PartTwo()
    {
        var score = 0;
        for (int i = 0; i < _input.Length; i++)
        {
            var choiceInRound = _input[i].Split(' ');
            var opponentPick = GetHandpickToEnum(choiceInRound[0]);
            var playerPick = GetHandpickToEnum(choiceInRound[1]);
            score += GetRoundScore(opponentPick, playerPick, GameType.Fixed);
        }

        return score.ToString();
    }

    private int GetRoundScore(Hand opponent, Hand player, GameType gameType)
    {
        Result result;

        switch (gameType)
        {
            case GameType.Normal:
                result = GetHandpickResult(opponent, player);
                break;
            default:
                var fixedGameResult = new Dictionary<Hand, Result>()
                 {
                     {Hand.Rock, Result.Lost},
                     {Hand.Paper, Result.Draw},
                     {Hand.Scissor, Result.Won},
                 };

                result = fixedGameResult[player];
                player = GetOutcomeFromOpponentHandpick(opponent, result);
                break;
        }

        var roundScore = GetRoundResultScore(result);
        roundScore += GetHandpickScore(player);

        return roundScore;
    }

    private Result GetHandpickResult(Hand opponent, Hand player)
    {
        var gameTable = new Dictionary<(Hand, Hand), Result>()
        {
            { (Hand.Rock, Hand.Rock), Result.Draw },
            { (Hand.Rock, Hand.Paper), Result.Won },
            { (Hand.Rock, Hand.Scissor), Result.Lost },

            { (Hand.Paper, Hand.Paper), Result.Draw },
            { (Hand.Paper, Hand.Scissor), Result.Won },
            { (Hand.Paper, Hand.Rock), Result.Lost },

            { (Hand.Scissor, Hand.Scissor), Result.Draw },
            { (Hand.Scissor, Hand.Rock), Result.Won },
            { (Hand.Scissor, Hand.Paper), Result.Lost },
         };

        return gameTable[(opponent, player)];
    }

    private Hand GetOutcomeFromOpponentHandpick(Hand pick, Result result)
    {
        Hand nextMove;

        switch (result)
        {
            case Result.Won:
                var winTable = new Dictionary<Hand, Hand>()
                 {
                    {Hand.Rock, Hand.Paper},
                    {Hand.Paper, Hand.Scissor},
                    {Hand.Scissor, Hand.Rock},
                 };

                nextMove = winTable[pick];
                break;
            case Result.Lost:
                var loseTable = new Dictionary<Hand, Hand>()
                 {
                    {Hand.Rock, Hand.Scissor},
                    {Hand.Paper, Hand.Rock},
                    {Hand.Scissor, Hand.Paper},
                 };

                nextMove = loseTable[pick];
                break;
            case Result.Draw:
            default:
                nextMove = pick;
                break;
        }

        return nextMove;
    }


    private Hand GetHandpickToEnum(string pick)
    {
        var pickToEnum = new Dictionary<string, Hand>()
         {
            {"A", Hand.Rock},
            {"B", Hand.Paper},
            {"C", Hand.Scissor},

            {"X", Hand.Rock},
            {"Y", Hand.Paper},
            {"Z", Hand.Scissor}
         };

        return pickToEnum[pick];
    }

    private int GetHandpickScore(Hand pick)
    {
        var pickWorth = new Dictionary<Hand, int>()
         {
             {Hand.Rock, 1},
             {Hand.Paper, 2},
             {Hand.Scissor, 3},
         };

        return pickWorth[pick];
    }

    private int GetRoundResultScore(Result result)
    {
        switch (result)
        {
            case Result.Won:
                return 6;
            case Result.Draw:
                return 3;
            default:
                return 0;
        }
    }

    private enum GameType
    {
        Normal,
        Fixed
    }

    private enum Result
    {
        Won,
        Lost,
        Draw,
    }

    private enum Hand
    {
        Rock,
        Paper,
        Scissor,
    }

    #endregion

    #region Optimized version

    [Benchmark]
    public string PartOneOptimized()
    {
        var answer = 0;
        for (int i = 0; i < _input.Length; i++)
        {
            answer += GetRoundScoreOptimized(_input[i]);
        }

        return answer.ToString();
    }

    [Benchmark]
    public string PartTwoOptimized()
    {
        var answer = 0;
        for (int i = 0; i < _input.Length; i++)
        {
            answer += GetRoundScoreFixedOutcomeOptimzed(_input[i]);
        }

        return answer.ToString();
    }

    // note: can be further optimized by precalculating the total points -> pick + outcome
    public int Rock = 1, Paper = 2, Scissor = 3;
    public int Win = 6, Draw = 3;

    private int GetRoundScoreOptimized(string input)
    {
        var possibleOutcome = new Dictionary<string, int>()
        {
            { "A X", Rock + Draw},      // rock vs rock + draw
            { "A Y", Paper + Win},      // rock vs paper + win
            { "A Z", Scissor},          // rock vs scissor + lose
                                        
            { "B Y", Paper + Draw},     // paper vs paper + draw
            { "B Z", Scissor + Win},    // paper vs scissor + win
            { "B X", Rock},             // paper vs rock + lose                    
                
            { "C Z", Scissor + Draw},   // scissor vs scissor + draw  
            { "C X", Rock + Win},       // scissor vs rock + win         
            { "C Y", Paper},            // scissor vs paper + lose
        };

        return possibleOutcome[input];
    }

    private int GetRoundScoreFixedOutcomeOptimzed(string input)
    {
        //X lose, Y draw, Z  win
        var possibleOutcome = new Dictionary<string, int>()
        {
            { "A X", Scissor},
            { "B X", Rock},
            { "C X", Paper},

            { "A Y", Rock + Draw},
            { "B Y", Paper + Draw},
            { "C Y", Scissor + Draw},

            { "A Z", Paper + Win},
            { "B Z", Scissor + Win},
            { "C Z", Rock + Win},
        };

        return possibleOutcome[input];
    }

    #endregion
}