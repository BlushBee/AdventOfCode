using AoC2022.Interfaces;
using System;
using System.Collections.Generic;
using static AoC2022.Helpers;

namespace AoC2022
{
    internal class Day2 : ISolution
    {
        private readonly InputTypes _inputTypes;
        private string[] _input;

        public Day2(InputTypes inputTypes)
        {
            _inputTypes = inputTypes;
            ProcessInput();
        }

        public void ProcessInput()
        {
            _input = ReadInputFromDataFolder("Day2", _inputTypes);
        }

        public void PartOne()
        {
            var score = 0;
            for (int i = 0; i < _input.Length; i++)
            {
                var choiceInRound = _input[i].Split(' ');
                var opponentPick = GetPickToEnum(choiceInRound[0]);
                var playerPick = GetPickToEnum(choiceInRound[1]);
                score += GetRoundResult(opponentPick, playerPick, GameType.Normal);
            }

            Console.WriteLine($"Day 2 Part #1: {score}");
        }

        public void PartTwo()
        {
            var score = 0;
            for (int i = 0; i < _input.Length; i++)
            {
                var choiceInRound = _input[i].Split(' ');
                var opponentPick = GetPickToEnum(choiceInRound[0]);
                var playerPick = GetPickToEnum(choiceInRound[1]);
                score += GetRoundResult(opponentPick, playerPick, GameType.Fixed);
            }

            Console.WriteLine($"Day 2 Part #2: {score}");
        }

        private int GetRoundResult(Hand opponent, Hand player, GameType gameType)
        {
            Result result;

            switch (gameType)
            {
                case GameType.Normal:
                    result = GetNormalGameResult(opponent, player);
                    break;
                default:
                    var fixedGameResult = new Dictionary<Hand, Result>()
                     {
                         {Hand.Rock, Result.Lost},
                         {Hand.Paper, Result.Draw},
                         {Hand.Scissor, Result.Won},
                     };

                    result = fixedGameResult[player];
                    player = GetFixedGamePick(opponent, result);
                    break;
            }  

            var roundScore = GetRoundScore(result);
            roundScore += GetPickScore(player);

            return roundScore;
        }

        private Result GetNormalGameResult(Hand opponent, Hand player)
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

        private Hand GetFixedGamePick(Hand pick, Result result)
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


        private Hand GetPickToEnum(string pick)
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

        private int GetPickScore(Hand pick)
        {
            var pickWorth = new Dictionary<Hand, int>()
             {
                 {Hand.Rock, 1},
                 {Hand.Paper, 2},
                 {Hand.Scissor, 3},
             };

            return pickWorth[pick];
        }

        private int GetRoundScore(Result result)
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
    }
}