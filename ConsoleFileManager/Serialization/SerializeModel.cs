using System;
using System.IO;

namespace ConsoleFileManager.Serialization
{
    public class SerializeModel
    {
        private string _filePath = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System));
        private ConsoleColor _colorFolder = ConsoleColor.Yellow;
        private ConsoleColor _colorFile = ConsoleColor.Green;
        public string FilePath { get => _filePath; set => _filePath = value; }

        public ConsoleColor ColorFolder { get => _colorFolder; set => _colorFolder = value; }
        public ConsoleColor ColorFile { get => _colorFile; set => _colorFile = value; }
    }
}
