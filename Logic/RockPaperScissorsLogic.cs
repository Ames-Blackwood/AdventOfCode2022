using Advent.Models;

namespace Advent.Logic 
{
    public class RockPaperScissorsLogic 
    {
        public RockPaperScissorsLogic()
        {
        }

        public void EvaluateStrategy(string? value)
        {
            Strategy strategy = new Strategy();
            strategy.Code = value;
            Decode(strategy);
            Score(strategy);            
        }

        private void Decode(Strategy strategy)
        {
            if (strategy.Code is not null && strategy.Code.Length == 3)
            {
                RockPaperScissorsEnum tmp;
                if (Maps.MapOpponentAction.TryGetValue(strategy.Code.Substring(0,1), out tmp)) strategy.OpponentAction = tmp;
                if (Maps.MapPlayerAction.TryGetValue(strategy.Code.Substring(2,1), out tmp)) strategy.PlayerAction = tmp;
            }
        }

        private void Score(Strategy strategy)
        {
            if (strategy.OpponentAction is not null && strategy.PlayerAction is not null)
            {
                strategy.StrategyScore = Calculations.StrategyValue(strategy.OpponentAction, strategy.PlayerAction);
            }
        }
    }
}