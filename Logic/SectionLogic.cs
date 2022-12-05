using Advent.Models;

namespace Advent.Logic 
{
    public class SectionLogic 
    {
        public SectionLogic()
        {
        }

        public bool DetermineFullInclusion(Coordinate[] elfPair)
        {
            
            if (elfPair is null) throw new ArgumentNullException(nameof(elfPair));
            if (elfPair.Length != 2) throw new ArgumentOutOfRangeException(nameof(elfPair));

            return false;               
        }
    }
}