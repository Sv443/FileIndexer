using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;


namespace BusinessLogic
{
    /// <summary>
    /// Synchronous indexation
    /// </summary>
    public static class Indexation
    {
        public static event EventHandler<string> OnError;

        public static List<FileInfo> ReadDirRecursive(string path, uint depth = 6)
        {
            List<FileInfo> fileInfoList = new List<FileInfo>();

            try
            {
                DirectoryInfo di = new DirectoryInfo(path);
                FileInfo[] files = di.GetFiles();

                foreach (FileInfo file in files)
                    fileInfoList.Add(file);

                DirectoryInfo[] dirs = di.GetDirectories();

                if(dirs == null || dirs.Length < 1)
                    return fileInfoList;

                foreach (DirectoryInfo dir in dirs)
                    if (depth > 0)
                        return ReadDirRecursive(dir.FullName, (depth - 1));
            }
            catch (Exception ex)
            {
                OnError?.Invoke(null, ex.Message);
            }

            return fileInfoList;
        }
    }

    /// <summary>
    /// Asynchronous indexation based on threading to optimize performance
    /// </summary>
    public class ThreadedIndexation
    {
        public event EventHandler<List<FileInfo>> OnFinish;
        public event EventHandler<string> OnError;

        private readonly Thread thread;
        private bool errored = false;

        public ThreadedIndexation(string path)
        {
            thread = new Thread(new ThreadStart(() => ThreadProc(path)));

            thread.Name = "ThreadedIndexation";
        }

        private void ThreadProc(string path)
        {
            Indexation.OnError += HandleError;

            List<FileInfo> fileList = Indexation.ReadDirRecursive(path);

            if (!errored)
                OnFinish?.Invoke(null, fileList);
            else
                HandleError(null, "Unknown error");
        }

        private void HandleError(object sender, string error)
        {
            errored = true;

            OnError?.Invoke(this, error);
        }

        /// <summary>
        /// Runs the indexation.
        /// Subscribe to the event OnFinish to receive the result.
        /// Subscribe to OnError to receive error messages.
        /// </summary>
        public void Run()
        {
            errored = false;
            thread.Start();
        }
    }
}
