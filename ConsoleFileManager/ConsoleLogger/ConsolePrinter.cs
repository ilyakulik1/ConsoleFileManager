using ConsoleFileManager.Calculation;
using ConsoleFileManager.Information;
using ConsoleFileManager.Serialization;
using System;
using System.IO;

namespace ConsoleFileManager.ConsoleLogger
{
    public class ConsolePrinter
    {
        private SerializeModel _model;
        public ConsolePrinter(SerializeModel model)
        {
            _model = model;
        }

        public void PrintDirectoryInfo(DirectoryInformation information)
        {
            WriteLine();
            WriteLine($"Время создания: {information.Directory.CreationTime}");
            WriteLine($"Время изменения: {information.Directory.LastWriteTime}");
            WriteLine($"Количество файлов: {information.Directory.GetFiles().Length}");
            WriteLine($"Количество директорий: {information.Directory.GetDirectories().Length}");
            WriteLine($"Размер файлов в каталоге: {new SizeGetter(information.Directory).GetFilesSize()} байт");
        }

        public void PrintList(DirectoryInformation information)
        {
            Console.Clear();
            IDesignation designation = new DirectoryDesignation();
            foreach (var item in information.Directory.EnumerateDirectories())
            {
                designation.Design(_model);
                Console.WriteLine(item.Name);
            }
            designation = new FileDesignation();
            foreach (var item in information.Directory.EnumerateFiles())
            {
                designation.Design(_model);
                Console.WriteLine(item.Name);
            }
        }

        public void PrintFilterAll(DirectoryInformation information, string filter)
        {
            Console.Clear();
            IDesignation designation = new DirectoryDesignation();
            foreach (var item in information.Directory.EnumerateDirectories(filter, SearchOption.AllDirectories))
            {
                designation.Design(_model);
                Console.WriteLine($"{item.Name} -> {item.Parent}");
            }
            designation = new FileDesignation();
            foreach (var item in information.Directory.EnumerateFiles(filter, SearchOption.AllDirectories))
            {
                designation.Design(_model);
                Console.WriteLine($"{item.Name} -> {item.DirectoryName}");
            }
        }
        public void PrintFilter(DirectoryInformation information, string filter)
        {
            Console.Clear();
            IDesignation designation = new DirectoryDesignation();
            foreach (var item in information.Directory.EnumerateDirectories(filter))
            {
                designation.Design(_model);
                Console.WriteLine(item.Name);
            }
            designation = new FileDesignation();
            foreach (var item in information.Directory.EnumerateFiles(filter))
            {
                designation.Design(_model);
                Console.WriteLine(item.Name);
            }
        }

        public static void LogError()
        {
            Console.WriteLine("Введена неправильная команда.");
        }

        public static void LogError(string message)
        {
            Console.WriteLine(message);
        }

        public static void LogNotFound(string name)
        {
            Console.WriteLine($"{name} не найден.");
        }


        public void WriteCommandField()
        {
            Console.Write($"\n{_model.FilePath}>");
        }

        public static void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
        public static void WriteLine()
        {
            Console.WriteLine();
        }
    }
}
