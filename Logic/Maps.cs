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

        public static IDictionary<string, RockPaperScissorsEnum> MapPlayerAction = new Dictionary<string, RockPaperScissorsEnum>{
            {"X", RockPaperScissorsEnum.Rock},
            {"Y", RockPaperScissorsEnum.Paper},
            {"Z", RockPaperScissorsEnum.Scissors}
        };
    }
}