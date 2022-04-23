using System.IO;

namespace ConsoleFileManager.Calculation
{
    public class SizeGetter
    {
        private long _sizeFolder;
        private DirectoryInfo _info;

        public SizeGetter(DirectoryInfo info)
        {
            _info = info;
        }
        public long GetFilesSize()
        {
            long size = 0;
            foreach (var item in _info.GetFiles())
            {
                size += item.Length;
            }
            return size;
        }

        public long GetFolderSize(DirectoryInfo info)
        {
            if (_info.FullName == info.FullName)
            {
                _sizeFolder = 0;
            }
            foreach (var item in info.GetFiles())
            {
                _sizeFolder += item.Length;
            }
            foreach (var item in info.GetDirectories())
            {
                GetFolderSize(item);
            }
            return _sizeFolder;
        }
    }
}