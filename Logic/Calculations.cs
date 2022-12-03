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
    }
}