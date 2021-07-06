using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class Daemon
    {
        private string path;
        private FileSystemWatcher watcher;

        public event EventHandler<FileInfo[]> onChange;

        public Daemon(string path, bool recursive = true)
        {
            this.path = path;

            watcher = new FileSystemWatcher(this.path);
            watcher.IncludeSubdirectories = recursive;
            watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
            watcher.Changed += OnFilesChanged;
        }

        private void OnFilesChanged(object sender, FileSystemEventArgs e)
        {
            FileInfo[] changedFilesList = { };

            // TODO

            onChange?.Invoke(this, changedFilesList);

            /*
            if (e.ChangeType != WatcherChangeTypes.Changed)
            {
                return;
            }
            Console.WriteLine($"Changed: {e.FullPath}");
            */
        }
    }
}
