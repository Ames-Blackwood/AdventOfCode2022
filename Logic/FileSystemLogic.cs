using Advent.Extensions;
using Advent.Models;
using Microsoft.Extensions.Logging;

namespace Advent.Logic 
{
    public class FileSystemLogic 
    {
        private readonly Folder _root;
        private Folder _currentFolder;
        private readonly ILogger<FileSystemLogic> _logger;
        public FileSystemLogic(ILogger<FileSystemLogic> logger)
        {
            _logger = logger;
            _root = new Folder();
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