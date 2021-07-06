using System;
using System.Collections.Generic;
using System.IO;


namespace BusinessLogic
{
    public class Indexation
    {
        public event EventHandler<string> onError;

        public Indexation()
        {

        }

        public List<FileInfo> ReadDirRecursive(string path)
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
                onError?.Invoke(this, ex.Message);
            }

            return fileInfoList;
        }
    }
}
