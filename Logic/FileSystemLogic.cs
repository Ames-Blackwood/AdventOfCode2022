using Advent.Extensions;
using Advent.Models;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Advent.Logic 
{
    public class FileSystemLogic 
    {
        private readonly Folder _root;
        public Folder Root { get => _root; }
        private Folder _currentFolder;
        private readonly ILogger<FileSystemLogic> _logger;
        private const string PREFIX_SPACER = "  ";

        public FileSystemLogic(ILogger<FileSystemLogic> logger)
        {
            _logger = logger;
            _root = new Folder {Name = @"/"};
            _currentFolder = _root;
        }

        public bool TryParseEntry(string entry)
        {
            if (entry is null || entry == "") throw new ArgumentNullException(nameof(entry));
            return entry.Substring(0,1) switch {
                "$" => TryParseCommandEntry(entry),
                "d" => TryParseFolderEntry(entry),
                _ => TryParseFileEntry(entry)
            };
        }

        public decimal DepthFirstConstrainedFolderSum(decimal maxSizeToSum, Folder? folder = null)
        {
            if (folder is null) folder = _root;
            if (folder is null) throw new ArgumentNullException(nameof(folder));

            var folders = folder.Folders.Values.ToList();
            decimal subSum = 0;
            if (folders.Count > 0) 
            {
                folders.ForEach(f => {
                    subSum += DepthFirstConstrainedFolderSum(maxSizeToSum, f);
                });
            }
            var size = folder.GetSize();
            return size <= maxSizeToSum
                ? size + subSum
                : subSum;
        }

        public decimal FindSpaceForInstall(decimal minimumSpaceToFind, Folder? folder = null)
        {
            if (folder is null) folder = _root;
            if (folder is null) throw new ArgumentNullException(nameof(folder));

            var folders = folder.Folders.Values.ToList();
            var currentMinimumSize = 0M;
            if (folders.Count > 0)
            {
                currentMinimumSize = folders.Select(f => FindSpaceForInstall(minimumSpaceToFind, f)).Where(i => i > minimumSpaceToFind).MinSafe().Min();
            }
            var currentFolderSize = folder.GetSize();
            return (currentFolderSize > minimumSpaceToFind && (currentMinimumSize == 0 || currentFolderSize < currentMinimumSize))
                ? currentFolderSize
                : currentMinimumSize;
        }


        public void WriteFolderContents(Folder? folder = null, string prefix = "")
        {
            var newPrefix = prefix + PREFIX_SPACER;
            if (folder is null) folder = _root;
            if (folder is null) throw new ArgumentNullException(nameof(folder));

            Console.WriteLine($"{prefix}- {folder.Name} (dir)");

            folder.Folders.Keys.ToList().ForEach(key => {
                if (folder.Folders.TryGetValue(key, out Folder? f) && f is not null)
                    WriteFolderContents(f, newPrefix);
            });

            folder.Files.Keys.ToList().ForEach(key => {
                if (folder.Files.TryGetValue(key, out decimal value))
                    Console.WriteLine ($"{newPrefix}- {key} (file, size={value})");
            });
        }

        private bool TryParseCommandEntry(string entry)
        {
            return entry.Substring(2,2) switch {
                "cd" => TryChangeDirectory(entry.Substring(5)),
                _ => true
            };
        }

        private bool TryChangeDirectory(string folderName)
        {
            Func<Folder?,bool> assignCurrentFolder = (folder) =>
            {
                if (folder is null) return false;
                _currentFolder = folder;
                return true;
            };

            Func<string,bool> tryAssignCurrentFolder = (folderName) =>
            {
                if (_currentFolder.Folders.TryGetValue(folderName, out var selectedFolder))
                    return assignCurrentFolder(selectedFolder);
                return false;
            };

            return folderName switch {
                ".." => assignCurrentFolder(_currentFolder.Parent),
                "/" => assignCurrentFolder(_root),
                _ => tryAssignCurrentFolder(folderName)
            };
        }

        private bool TryParseFileEntry(string entry)
        {
            decimal size = 0;
            string name = "";

            var tmp = entry.Split(" ", 2);

            size = decimal.Parse(tmp[0]);
            name = tmp[1];
            
            return _currentFolder.TryAddFile(name, size);
        }
        private bool TryParseFolderEntry(string entry)
        {
            var name = entry.Split(" ", 2).Last();
            return _currentFolder.TryAddFolder(name);
        }
    }
}