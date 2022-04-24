using ConsoleFileManager.Serialization;
using System;
using System.IO;
using ConsoleFileManager.ConsoleLogger;
using ConsoleFileManager.Information;
using ConsoleFileManager.Commands.Interface;

namespace ConsoleFileManager.Commands
{
    public class CopyCommand : ICommand
    {
        public void Execute(CommandArgumentsGetter commands, SerializeModel model, ConsolePrinter console, DirectoryInformation information)
        {
            var dirs = information.GetDirectoyInfoFromArgument(commands.Arguments[1]);
            if (dirs != null)
            {
                CopyDirectory(Path.Combine(model.FilePath, dirs.Name), Path.Combine(model.FilePath, commands.Arguments[2]));
                console.PrintList(information);
                console.PrintDirectoryInfo(information);
                return;
            }
            var files = information.GetFileInfoFromArgument(commands.Arguments[1]);
            if (files != null)
            {
                try
                {
                    File.Copy(Path.Combine(model.FilePath, files.Name), Path.Combine(model.FilePath, commands.Arguments[2]), true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Не удается скопировать файл");
                    return;
                }
                console.PrintList(information);
                console.PrintDirectoryInfo(information);
                return;
            }
            ConsolePrinter.LogNotFound(commands.Arguments[1]);
        }

        private void CopyDirectory(string path, string pathCopy)
        {
            try
            {
                if (path == pathCopy)
                {
                    Console.WriteLine("Такая папка уже имеется в списке");
                    Console.ReadKey(true);
                    return;
                }
                Directory.CreateDirectory(pathCopy);
                var dirs = Directory.GetDirectories(path);
                var files = Directory.GetFiles(path);
                for (int i = 0; i < files.Length; i++)
                {
                    File.Copy(files[i], Path.Combine(pathCopy, Path.GetFileName(files[i])), true);
                }
                for (int i = 0; i < dirs.Length; i++)
                {
                    CopyDirectory(Path.Combine(path, dirs[i]), Path.Combine(pathCopy, Path.GetFileName(dirs[i])));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
