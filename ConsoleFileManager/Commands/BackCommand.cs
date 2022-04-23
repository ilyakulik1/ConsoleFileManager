using ConsoleFileManager.Serialization;
using System.IO;
using ConsoleFileManager.ConsoleLogger;
using ConsoleFileManager.Information;
using ConsoleFileManager.Commands.Interface;

namespace ConsoleFileManager.Commands
{
    public class BackCommand : ICommand
    {
        XmlSerialize xmlSerialize = XmlSerialize.GetInstance();
        public void Execute(CommandArgumentsGetter commands, SerializeModel model, ConsolePrinter console, DirectoryInformation information)
        {
            if (Directory.GetParent(model.FilePath) != null)
            {
                model.FilePath = Directory.GetParent(model.FilePath).FullName;
            }
            information.Directory = new DirectoryInfo(model.FilePath);
            console.PrintList(information);
            console.PrintDirectoryInfo(information);
            xmlSerialize.Serialize(model);
        }
    }
}
