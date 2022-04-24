using ConsoleFileManager.ConsoleLogger;
using ConsoleFileManager.Information;
using ConsoleFileManager.Serialization;

namespace ConsoleFileManager.Commands.Interface
{
    public interface ICommand
    {
        void Execute(CommandArgumentsGetter commands, SerializeModel model, ConsolePrinter console, DirectoryInformation information);
    }
}
