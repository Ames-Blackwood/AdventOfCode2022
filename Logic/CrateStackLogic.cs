using Microsoft.Extensions.Logging;

namespace Advent.Logic 
{
    public class CrateStackLogic 
    {
        private readonly ILogger<CrateStackLogic> _logger;
        public CrateStackLogic(ILogger<CrateStackLogic> logger)
        {
            _logger = logger;
        }

        public void ModelStack(List<string> stackBuffer)
        {
            throw new NotImplementedException();
        }

        public void UpdateStack(string instruction)
        {
            throw new NotImplementedException();
        }

        public string GetTopItems()
        {
            throw new NotImplementedException();
        }
    }
}