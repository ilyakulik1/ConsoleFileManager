using ConsoleFileManager.Commands.Interface;
using ConsoleFileManager.ConsoleLogger;
using ConsoleFileManager.Information;
using ConsoleFileManager.Serialization;
using System;
using System.IO;

namespace ConsoleFileManager
{
    internal class NewFileCommand : ICommand
    {
        public void Execute(CommandArgumentsGetter commands, SerializeModel model, ConsolePrinter console, DirectoryInformation information)
        {
            File.Create(Path.Combine(model.FilePath, commands.Arguments[1]));
            console.PrintList(information);
            console.PrintDirectoryInfo(information);
        }
    }
}
