using ConsoleFileManager.Commands.Interface;
using ConsoleFileManager.ConsoleLogger;
using ConsoleFileManager.Information;
using ConsoleFileManager.Serialization;
using System.IO;

namespace ConsoleFileManager.Commands
{
    public class SetAtributeCommand : ICommand
    {
        public void Execute(CommandArgumentsGetter commands, SerializeModel model, ConsolePrinter console, DirectoryInformation information)
        {
            var file = information.GetFileInfoFromArgument(commands.Arguments[1]);
            if (file != null)
            {
                FileAttributes attributes = File.GetAttributes(file.FullName);
                switch (commands.Arguments[2])
                {
                    case "Hidden":
                        File.SetAttributes(file.FullName, attributes | FileAttributes.Hidden);
                        break;
                    case "ReadOnly":
                        File.SetAttributes(file.FullName, attributes | FileAttributes.ReadOnly);
                        break;
                    case "Archive":
                        File.SetAttributes(file.FullName, attributes | FileAttributes.Archive);
                        break;
                    case "System":
                        File.SetAttributes(file.FullName, attributes | FileAttributes.System);
                        break;
                    default:
                        ConsolePrinter.LogError("Введен неверный аттрибут");
                        break;
                }
            }
        }
    }
}
