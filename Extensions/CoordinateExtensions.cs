using Advent.Models;

namespace Advent.Extensions
{
    public static class CoordinateExtensions
    {
        public static bool Contains (this Coordinate input, Coordinate test){
            return (input.Min <= test.Min && input.Max >= test.Max);
        }
    }
}