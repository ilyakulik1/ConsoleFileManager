using ConsoleFileManager.Commands.Interface;
using ConsoleFileManager.ConsoleLogger;
using ConsoleFileManager.Information;
using ConsoleFileManager.Serialization;
using System.IO;

namespace ConsoleFileManager.Commands
{
    public class MkDirCommand : ICommand
    {

        public void Execute(CommandArgumentsGetter commands, SerializeModel model, ConsolePrinter console, DirectoryInformation information)
        {
            Directory.CreateDirectory(Path.Combine(model.FilePath, commands.Arguments[1]));
            console.PrintList(information);
            console.PrintDirectoryInfo(information);
        }
    }
}
