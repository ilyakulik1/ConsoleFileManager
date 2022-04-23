using ConsoleFileManager.Commands.Interface;
using ConsoleFileManager.ConsoleLogger;
using ConsoleFileManager.Information;
using ConsoleFileManager.Serialization;

namespace ConsoleFileManager.Commands
{
    public class UnknownCommand : ICommand
    {
        public void Execute(CommandArgumentsGetter commands, SerializeModel model, ConsolePrinter console, DirectoryInformation information)
        {
            ConsolePrinter.LogError();
        }
    }
}
