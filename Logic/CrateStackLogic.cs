using Microsoft.Extensions.Logging;

namespace Advent.Logic 
{
    public class CrateStackLogic 
    {
        private readonly ILogger<CrateStackLogic> _logger;

        private Stack<string>[] _stacks = null;
        private int _stackCount = 0;
        public CrateStackLogic(ILogger<CrateStackLogic> logger)
        {
            _logger = logger;
        }

        public void ModelStack(List<string> stackBuffer)
        {
            if (stackBuffer is null || stackBuffer.Count < 1) throw new ArgumentException(nameof(stackBuffer));
            stackBuffer.Reverse();

            var stackNumberRow = true;

            foreach (var row in stackBuffer)
            {
                if (stackNumberRow)
                {
                    stackNumberRow = false;
                    var stackCountString = stackBuffer.First().Split(" ").Last(i => i.Trim() != "");
                    _stackCount = int.Parse(stackCountString);

                    _stacks = new Stack<string>[_stackCount];
                }
                else
                {
                    StackRow(row);
                }
            }
        }

        public void UpdateStack(string instruction)
        {
            throw new NotImplementedException();
        }

        public string GetTopItems()
        {
            throw new NotImplementedException();
        }

        private void StackRow(string row)
        {
            throw new NotImplementedException();
        }
    }
}