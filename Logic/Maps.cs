using Advent.Models;

namespace Advent.Logic 
{
    public static class Maps 
    {
        public static IDictionary<string, RockPaperScissorsEnum> MapOpponentAction = new Dictionary<string, RockPaperScissorsEnum>{
            {"A", RockPaperScissorsEnum.Rock},
            {"B", RockPaperScissorsEnum.Paper},
            {"C", RockPaperScissorsEnum.Scissors}
        };

        public static IDictionary<string, WinLoseDrawEnum> MapDesiredResult = new Dictionary<string, WinLoseDrawEnum>{
            {"X", WinLoseDrawEnum.Lose},
            {"Y", WinLoseDrawEnum.Draw},
            {"Z", WinLoseDrawEnum.Win}
        };
    }
}