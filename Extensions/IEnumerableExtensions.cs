namespace Advent.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<decimal> MinSafe(this IEnumerable<decimal> current)
        {
            return current is not null && current?.Count() > 0
                ? current
                : new List<decimal>{0M};
        }
    }
}