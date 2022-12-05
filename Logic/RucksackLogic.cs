namespace Advent.Logic 
{
    public class RucksackLogic 
    {
        public RucksackLogic()
        {
        }

        public int FindPriorityOfDuplicates(string rucksackContents){
            
            if (rucksackContents is null) throw new ArgumentNullException(nameof(rucksackContents));
                       
            int totalItems = rucksackContents.Trim().Length;
            
            if (totalItems % 2 != 0) throw new ArgumentOutOfRangeException(nameof(rucksackContents));

            int compartmentSize = totalItems / 2;
            char[] compartment1 = rucksackContents.Substring(0,compartmentSize).ToCharArray();
            char[] compartment2 = rucksackContents.Substring(compartmentSize).ToCharArray();

            char? duplicate = compartment1.Intersect(compartment2).First();
            
            return duplicate is null
                ? 0
                : Calculations.CalculatePriority(duplicate);
        }
    }
}