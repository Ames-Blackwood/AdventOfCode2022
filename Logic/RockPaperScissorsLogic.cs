using Advent.Models;

namespace Advent.Logic 
{
    public class RockPaperScissorsLogic 
    {
        public RockPaperScissorsLogic()
        {
        }

        public Strategy EvaluateStrategy(string? value)
        {
            Strategy strategy = new Strategy();
            strategy.Code = value;
            Decode(strategy);
            Score(strategy);            
            return strategy;
        }

        private void Decode(Strategy strategy)
        {
            if (strategy.Code is not null && strategy.Code.Length == 3)
            {
                RockPaperScissorsEnum tmpOpponentAction;
                if (Maps.MapOpponentAction.TryGetValue(strategy.Code.Substring(0,1), out tmpOpponentAction)) strategy.OpponentAction = tmpOpponentAction;
                WinLoseDrawEnum tmpDesiredOutcome;
                if (Maps.MapDesiredResult.TryGetValue(strategy.Code.Substring(2,1), out tmpDesiredOutcome)) strategy.DesiredOutcome = tmpDesiredOutcome;
            }
        }

        private void Score(Strategy strategy)
        {
            if (strategy.OpponentAction is not null && strategy.DesiredOutcome is not null)
            {
                strategy.StrategyScore = Calculations.StrategyValueFromDesiredOutcome(strategy.OpponentAction, strategy.DesiredOutcome);
            }
        }
    }
}