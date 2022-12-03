using Advent.Models;

namespace Advent.Logic 
{
    public static class Calculations 
    {
        private static Func<RockPaperScissorsEnum?, int> _actionValue = (value)=> (int?)(value) ?? 0;

        public static Func<RockPaperScissorsEnum?, RockPaperScissorsEnum?, int> StrategyValue = (opponent, player) => 
        {
            return _actionValue(player) + (int) (opponent switch {
                // Opponent plays Paper
                RockPaperScissorsEnum.Paper => player switch {
                    RockPaperScissorsEnum.Paper => WinLoseDrawEnum.Draw,
                    RockPaperScissorsEnum.Rock => WinLoseDrawEnum.Lose,
                    RockPaperScissorsEnum.Scissors => WinLoseDrawEnum.Win,
                    _ => throw new NotImplementedException()
                },
                // Opponent plays Rock
                RockPaperScissorsEnum.Rock => player switch {
                    RockPaperScissorsEnum.Paper => WinLoseDrawEnum.Win,
                    RockPaperScissorsEnum.Rock => WinLoseDrawEnum.Draw,
                    RockPaperScissorsEnum.Scissors => WinLoseDrawEnum.Lose,
                    _ => throw new NotImplementedException()
                },
                // Opponent plays Scissors
                RockPaperScissorsEnum.Scissors => player switch {
                    RockPaperScissorsEnum.Paper => WinLoseDrawEnum.Lose,
                    RockPaperScissorsEnum.Rock => WinLoseDrawEnum.Win,
                    RockPaperScissorsEnum.Scissors => WinLoseDrawEnum.Draw,
                    _ => throw new NotImplementedException()
                },
                _ => throw new NotImplementedException()
            });
        };

        public static Func<RockPaperScissorsEnum?, WinLoseDrawEnum?, int> StrategyValueFromDesiredOutcome = (opponent, desiredOutcome) => 
        {
            return opponent switch {
                // Opponent plays Paper
                RockPaperScissorsEnum.Paper => desiredOutcome switch {
                    WinLoseDrawEnum.Draw => StrategyValue(opponent, RockPaperScissorsEnum.Paper),
                    WinLoseDrawEnum.Lose => StrategyValue(opponent, RockPaperScissorsEnum.Rock),
                    WinLoseDrawEnum.Win => StrategyValue(opponent, RockPaperScissorsEnum.Scissors),
                    _ => throw new NotImplementedException()
                },
                // Opponent plays Rock
                RockPaperScissorsEnum.Rock => desiredOutcome switch {
                    WinLoseDrawEnum.Draw => StrategyValue(opponent, RockPaperScissorsEnum.Rock),
                    WinLoseDrawEnum.Lose => StrategyValue(opponent, RockPaperScissorsEnum.Scissors),
                    WinLoseDrawEnum.Win => StrategyValue(opponent, RockPaperScissorsEnum.Paper),
                    _ => throw new NotImplementedException()
                },
                // Opponent plays Scissors
                RockPaperScissorsEnum.Scissors => desiredOutcome switch {
                    WinLoseDrawEnum.Draw => StrategyValue(opponent, RockPaperScissorsEnum.Scissors),
                    WinLoseDrawEnum.Lose => StrategyValue(opponent, RockPaperScissorsEnum.Paper),
                    WinLoseDrawEnum.Win => StrategyValue(opponent, RockPaperScissorsEnum.Rock),
                    _ => throw new NotImplementedException()
                },
                _ => throw new NotImplementedException()
            };
        };
    }
}