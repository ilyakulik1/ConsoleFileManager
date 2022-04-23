using ConsoleFileManager.Serialization;
using System.IO;
using ConsoleFileManager.ConsoleLogger;
using ConsoleFileManager.Information;
using ConsoleFileManager.Commands.Interface;

namespace ConsoleFileManager.Commands
{
    public class DeleteCommand : ICommand
    {
        public void Execute(CommandArgumentsGetter commands, SerializeModel model, ConsolePrinter console, DirectoryInformation information)
        {
            var dir = information.GetDirectoyInfoFromArgument(commands.Arguments[1]);
            if (dir != null)
            {
                Directory.Delete(Path.Combine(model.FilePath, commands.Arguments[1]), true);
                console.PrintList(information);
                console.PrintDirectoryInfo(information);
                return;
            }
            var file = information.GetFileInfoFromArgument(commands.Arguments[1]);
            if (file != null)
            {
                File.Delete(Path.Combine(model.FilePath, commands.Arguments[1]));
                console.PrintList(information);
                console.PrintDirectoryInfo(information);
                return;
            }
            ConsolePrinter.LogNotFound(commands.Arguments[1]);
        }
    }
}
