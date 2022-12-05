namespace Advent.Logic 
{
    public static class Calculations 
    {
        public static Func<char?, int> CalculatePriority = (value) => 
        {
            if (value is null) throw new ArgumentNullException(nameof(value));
            int asciiValue = (int) value;
            return asciiValue > 90
                ? asciiValue - 96
                : asciiValue - 38;
        };
    }
}