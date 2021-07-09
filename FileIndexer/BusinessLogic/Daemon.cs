using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public struct FileChange
    {
        public FileInfo file;
        public WatcherChangeTypes type;
    }

    public class Daemon
    {
        private string path;
        private FileSystemWatcher watcher;

        public event EventHandler<FileChange> onChange;

        public Daemon(string path, bool recursive = true, string filter = "*.*")
        {
            this.path = path;

            watcher = new FileSystemWatcher(this.path);

            watcher.IncludeSubdirectories = recursive;
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.Filter = filter;

            watcher.Changed += OnFilesChanged;
            /*watcher.Created += OnFilesChanged;
            watcher.Deleted += OnFilesChanged;
            watcher.Renamed += OnFilesChanged;*/

            watcher.EnableRaisingEvents = true;
        }

        private void OnFilesChanged(object sender, FileSystemEventArgs e)
        {
            FileChange change = new FileChange();
            change.file = new FileInfo(e.FullPath);

            onChange?.Invoke(this, change);
        }

        /// <summary>
        /// Disposes of the daemon's FileSystemWatcher
        /// </summary>
        public void Dispose()
        {
            watcher.Changed -= OnFilesChanged;
            this.watcher.Dispose();
        }
    }
}
