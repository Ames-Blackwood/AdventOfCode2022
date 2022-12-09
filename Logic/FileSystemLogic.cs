using Advent.Models;
using Microsoft.Extensions.Logging;

namespace Advent.Logic 
{
    public class FileSystemLogic 
    {
        private readonly ILogger<FileSystemLogic> _logger;
        public FileSystemLogic(ILogger<FileSystemLogic> logger)
        {
            _logger = logger;
        }
    }
}