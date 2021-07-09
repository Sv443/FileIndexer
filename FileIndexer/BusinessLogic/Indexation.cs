using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;


namespace BusinessLogic
{
    public static class Indexation
    {
        public static event EventHandler<string> onError;

        public static List<FileInfo> ReadDirRecursive(string path)
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
                    return ReadDirRecursive(dir.FullName);
            }
            catch (Exception ex)
            {
                onError?.Invoke(null, ex.Message);
            }

            return fileInfoList;
        }
    }

    public class ThreadedIndexation
    {
        public static event EventHandler onFinish;

        private Thread thread;

        ThreadedIndexation(string path)
        {
            thread = new Thread(new ThreadStart(() => ThreadProc(path)));
        }

        /// <summary>
        /// Gets executed when the thread starts
        /// </summary>
        public static void ThreadProc(string path)
        {
            List<FileInfo> fileList = Indexation.ReadDirRecursive(path);
        }

        /// <summary>
        /// Runs the thread
        /// </summary>
        public void Run()
        {
            thread.Start();
        }
    }
}
