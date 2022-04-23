using ConsoleFileManager.Serialization;
using System;
using System.IO;

namespace ConsoleFileManager.Information
{
    public class DirectoryInformation
    {
        public DirectoryInfo Directory { get; set; }

        public DirectoryInformation(SerializeModel model)
        {
            Directory = new DirectoryInfo(model.FilePath);
        }

        public DirectoryInfo GetDirectoyInfoFromArgument(string arg)
        {
            foreach (var item in Directory.GetDirectories())
            {
                if (item.Name == arg)
                {
                    return item;
                }
            }
            return null;
        }

        public FileInfo GetFileInfoFromArgument(string arg)
        {
            foreach (var item in Directory.GetFiles())
            {
                if (item.Name == arg)
                {
                    return item;
                }
            }
            return null;
        }
    }
}
