using ConsoleFileManager.Commands.Interface;
using ConsoleFileManager.ConsoleLogger;
using ConsoleFileManager.Information;
using ConsoleFileManager.Serialization;

namespace ConsoleFileManager.Commands
{
    public class ClearCommand : ICommand
    {
        public void Execute(CommandArgumentsGetter commands, SerializeModel model, ConsolePrinter console, DirectoryInformation information)
        {
            console.PrintList(information);
            console.PrintDirectoryInfo(information);
        }
    }
}
