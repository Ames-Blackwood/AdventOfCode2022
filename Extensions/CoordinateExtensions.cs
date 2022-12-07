using Advent.Models;

namespace Advent.Extensions
{
    public static class CoordinateExtensions
    {
        public static bool Contains (this Coordinate input, Coordinate test){
            return (input.Min <= test.Min && input.Max >= test.Max);
        }
        public static bool Overlaps (this Coordinate input, Coordinate test){
            return (input.Min <= test.Min && input.Max >= test.Min)
                || (input.Min <= test.Max && input.Max >= test.Max);
        }
    }
}