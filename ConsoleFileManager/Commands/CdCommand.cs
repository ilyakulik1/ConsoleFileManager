using ConsoleFileManager.Serialization;
using System.IO;
using ConsoleFileManager.ConsoleLogger;
using ConsoleFileManager.Information;
using ConsoleFileManager.Commands.Interface;

namespace ConsoleFileManager.Commands
{
    public class CdCommand : ICommand
    {
        XmlSerialize xmlSerialize = XmlSerialize.GetInstance();
        public void Execute(CommandArgumentsGetter commands, SerializeModel model, ConsolePrinter console, DirectoryInformation information)
        {
            string combine = Path.Combine(model.FilePath, commands.Arguments[1]);
            if (Directory.Exists(combine))
            {
                model.FilePath = Path.Combine(model.FilePath, commands.Arguments[1]);
                information.Directory = new DirectoryInfo(model.FilePath);
                console.PrintList(information);
                console.PrintDirectoryInfo(information);
                xmlSerialize.Serialize(model);
            }
            else
            {
                ConsolePrinter.LogNotFound(commands.Arguments[1]);
            }
        }
    }
}
