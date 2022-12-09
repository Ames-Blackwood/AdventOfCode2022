using Advent.Models;

namespace Advent.Extensions
{
    public static class FolderExtensions
    {
        public static bool TryAddFolder(this Folder parent, string newFolderName)
        {
            return parent.Folders.TryAdd(newFolderName, new Folder());
        }

        public static bool TryAddFile(this Folder parent, string newFileName, decimal size)
        {
            return parent.Files.TryAdd(newFileName, size);
        }
    }
}