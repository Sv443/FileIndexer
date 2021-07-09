using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    /// <summary>
    /// Describes a changed file
    /// </summary>
    public struct FileChange
    {
        public FileInfo file;
    }

    /// <summary>
    /// Listens for file changes in a set directory to invoke an event
    /// </summary>
    public class Daemon
    {
        private string path;
        private FileSystemWatcher watcher;

        public event EventHandler<FileChange> onChange;

        /// <summary>
        /// Creates an instance of the Daemon class
        /// </summary>
        /// <param name="path">Path to the directory to supervise</param>
        /// <param name="recursive">Whether or not to recursively listen for changes</param>
        /// <param name="filter">A pattern of which files to listen for changes on</param>
        public Daemon(string path, bool recursive = true, string filter = "*.*")
        {
            this.path = path;

            watcher = new FileSystemWatcher(this.path);

            watcher.IncludeSubdirectories = recursive;
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.Filter = filter;

            watcher.Changed += OnFileChanged;
            /*
            watcher.Created += OnFileChanged;
            watcher.Deleted += OnFileChanged;
            watcher.Renamed += OnFileChanged;
            */

            watcher.EnableRaisingEvents = true;
        }

        /// <summary>
        /// Destructs this Daemon instance
        /// </summary>
        ~Daemon()
        {
            this.Dispose();
        }

        /// <summary>
        /// Called whenever a file has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnFileChanged(object sender, FileSystemEventArgs e)
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
            watcher.Changed -= OnFileChanged;
            this.watcher.Dispose();
        }
    }
}
