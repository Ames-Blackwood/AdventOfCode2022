using Advent.Models;
using Advent.Extensions;
using Microsoft.Extensions.Logging;

namespace Advent.Logic 
{
    public class ExampleLogic 
    {
        private readonly ILogger<ExampleLogic> _logger;
        public ExampleLogic(ILogger<ExampleLogic> logger)
        {
            _logger = logger;
        }
    }
}