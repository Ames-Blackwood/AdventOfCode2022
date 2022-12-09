namespace Advent.Models
{
    public class Folder
    {
        public string Name { get; set; } = "default";
        public IList<File> FileList { get; set; } = new List<File>();
        public IList<Folder> FolderList {get; set; } = new List<Folder>();
    }
}