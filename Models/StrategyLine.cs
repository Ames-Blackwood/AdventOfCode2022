namespace Advent.Models
{
    public class Strategy 
    {
        public RockPaperScissorsEnum? OpponentAction { get; set; }
        public RockPaperScissorsEnum? PlayerAction { get; set; }
        public int? StrategyScore { get; set; }
        public string? Code { get; set; }
    }
}