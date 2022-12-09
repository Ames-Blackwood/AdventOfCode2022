using Advent.Models;

namespace Advent.Extensions
{
    public static class FolderExtensions
    {
        public static bool TryAddFolder(this Folder current, string newFolderName)
        {
            return current.Folders.TryAdd(newFolderName, new Folder { Parent = current });
        }

        public static bool TryAddFile(this Folder current, string newFileName, decimal size)
        {
            return current.Files.TryAdd(newFileName, size);
        }

        public static decimal GetSize(this Folder current)
        {
            var fileSize = current.Files.Values.Sum();
            var folderSize = current.Folders.Select(i => i.Value.GetSize()).Sum();

            return fileSize + folderSize;
        }
    }
}