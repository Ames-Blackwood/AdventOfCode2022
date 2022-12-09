namespace Advent.Models
{
    public class Folder
    {
        public Folder? Parent { get; set; }
        public IDictionary<string,decimal> Files { get; set; } = new Dictionary<string,decimal>();
        public IDictionary<string,Folder> Folders {get; set; } = new Dictionary<string,Folder>();
    }
}