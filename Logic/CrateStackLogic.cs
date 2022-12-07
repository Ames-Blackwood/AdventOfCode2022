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
    }
}