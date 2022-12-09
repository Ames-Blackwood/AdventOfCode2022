using Advent.Models;

namespace Advent.Extensions
{
    public static class ExampleExtensions
    {
        public static bool Contains (this Example input, Example test){
            return (input.Min <= test.Min && input.Max >= test.Max);
        }
    }
}