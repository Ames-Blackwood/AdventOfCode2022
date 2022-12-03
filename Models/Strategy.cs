namespace Advent.Models
{
    public class Strategy 
    {
        public RockPaperScissorsEnum? OpponentAction { get; set; }
        public WinLoseDrawEnum? DesiredOutcome { get; set; }
        public int? StrategyScore { get; set; }
        public string? Code { get; set; }
    }
}