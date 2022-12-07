using Advent.Models;
using Advent.Extensions;
using Microsoft.Extensions.Logging;

namespace Advent.Logic 
{
    public class SectionLogic 
    {
        private readonly ILogger<SectionLogic> _logger;
        public SectionLogic(ILogger<SectionLogic> logger)
        {
            _logger = logger;
        }

        public bool DecodeAndDetermineFullInclusion(string codedString)
        {
            var result = DetermineOverlap(Decode(codedString));
            var testResult = DetermineFullInclusion(Decode(codedString));
            _logger.LogInformation ($"{(result ? "OVERLAP" : ""):6}: {(testResult ? "CONTAINS" : ""):8}: {codedString}");
            return result;
        }

        private bool DetermineFullInclusion(Coordinate[] elfPair)
        {
            if (elfPair is null) throw new ArgumentNullException(nameof(elfPair));
            if (elfPair.Length != 2) throw new ArgumentOutOfRangeException(nameof(elfPair));

            return elfPair[0].Contains(elfPair[1]) || elfPair[1].Contains(elfPair[0]);
        }
        private bool DetermineOverlap(Coordinate[] elfPair)
        {
            if (elfPair is null) throw new ArgumentNullException(nameof(elfPair));
            if (elfPair.Length != 2) throw new ArgumentOutOfRangeException(nameof(elfPair));

            return elfPair[0].Overlaps(elfPair[1]);
        }



        private Coordinate[] Decode(string codedString)
        {
            if (codedString == null) throw new ArgumentNullException(nameof(codedString));
            Coordinate[] elfPair = new Coordinate[2];
            
            var stringPair = codedString.Split(',');
            if (stringPair.Length != 2) throw new ArgumentOutOfRangeException(nameof(codedString));

            var minMax1 = stringPair[0].Split('-');
            if (minMax1.Length != 2) throw new ArgumentOutOfRangeException(nameof(codedString));

            var minMax2 = stringPair[1].Split('-');
            if (minMax2.Length != 2) throw new ArgumentOutOfRangeException(nameof(codedString));

            return new Coordinate[2] {
                new Coordinate { Min=int.Parse(minMax1[0]), Max=int.Parse(minMax1[1]) },
                new Coordinate { Min=int.Parse(minMax2[0]), Max=int.Parse(minMax2[1]) }
            };
        }
    }
}