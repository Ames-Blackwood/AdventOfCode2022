using System.Text;
using Microsoft.Extensions.Logging;

namespace Advent.Logic 
{
    public class CrateStackLogic 
    {
        private readonly ILogger<CrateStackLogic> _logger;

        private Stack<string>[]? _stacks;
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
                    for (var i = 0; i < _stackCount; i++)
                    {
                        _stacks[i] = new Stack<string>();
                    }
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
            if (_stacks is null) throw new ArgumentNullException(nameof(_stacks));

            var aggregator = new StringBuilder();
            for (var i = 0; i < _stackCount; i++)
            {
                var success = _stacks[i].TryPeek(out var item);
                aggregator.Append(success ? item : " ");
            }
            return aggregator.ToString();
        }

        private void StackRow(string row)
        {
            if (_stacks is null) throw new ArgumentNullException(nameof(_stacks));

            for (var i = 0; i < _stackCount; i++)
            {
                var item = row.Substring(GetPositionFromIndex(i),1).Trim();
                if (item != ""){
                    _stacks[i].Push(item);
                }
            }
        }

        private int GetPositionFromIndex(int index) => ((index + 1) * 4) - 3;
    }
}