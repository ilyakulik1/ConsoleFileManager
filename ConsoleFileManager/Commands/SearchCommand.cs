using ConsoleFileManager.Commands.Interface;
using ConsoleFileManager.ConsoleLogger;
using ConsoleFileManager.Information;
using ConsoleFileManager.Serialization;
using System;

namespace ConsoleFileManager.Commands
{
    public class SearchCommand : ICommand
    {
        public void Execute(CommandArgumentsGetter commands, SerializeModel model, ConsolePrinter console, DirectoryInformation information)
        {
            console.PrintFilter(information, commands.Arguments[1]);
            ConsolePrinter.WriteLine("Для возврата нажмите любую клавишу");
            Console.ReadKey(true);
            console.PrintList(information);
            console.PrintDirectoryInfo(information);
        }
    }
}
