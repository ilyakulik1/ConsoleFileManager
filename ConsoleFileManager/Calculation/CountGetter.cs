using System.IO;

namespace ConsoleFileManager.Calculation
{
    public class CountGetter
    {
        private int _count;
        private DirectoryInfo _info;

        public CountGetter(DirectoryInfo info)
        {
            _info = info;
        }
        public int GetFilesCount(DirectoryInfo directoryInfo)
        {
            if (_info.FullName == directoryInfo.FullName)
            {
                _count = 0;
            }
            _count += directoryInfo.GetFiles().Length;
            foreach (var item in directoryInfo.GetDirectories())
            {
                GetFilesCount(item);
            }
            return _count;
        }

        public int GetFolderCount(DirectoryInfo directoryInfo)
        {
            if (_info.FullName == directoryInfo.FullName)
            {
                _count = 0;
            }
            _count += directoryInfo.GetDirectories().Length;
            foreach (var item in directoryInfo.GetDirectories())
            {
                GetFolderCount(item);
            }
            return _count;
        }
    }
}
