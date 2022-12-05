namespace Advent.Logic 
{
    public class RucksackLogic 
    {
        public RucksackLogic()
        {
        }

        public int FindPriorityOfBadge(List<string> rucksacks){
            
            if (rucksacks is null) throw new ArgumentNullException(nameof(rucksacks));
            if (rucksacks.Count != 3) throw new ArgumentOutOfRangeException(nameof(rucksacks));
                       
            char[][] characterArrayRucksacks = rucksacks.Select(i => i.ToCharArray()).ToArray();
            char? badge = characterArrayRucksacks[0]
                .Intersect(characterArrayRucksacks[1])
                .Intersect(characterArrayRucksacks[2])
                .First();

            return badge is null
                ? 0
                : Calculations.CalculatePriority(badge);
        }
    }
}