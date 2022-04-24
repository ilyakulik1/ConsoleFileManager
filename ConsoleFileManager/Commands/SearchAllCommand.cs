using ConsoleFileManager.Commands.Interface;
using ConsoleFileManager.ConsoleLogger;
using ConsoleFileManager.Information;
using ConsoleFileManager.Serialization;
using System;

namespace ConsoleFileManager.Commands
{
    public class SearchAllCommand : ICommand
    {
        public void Execute(CommandArgumentsGetter commands, SerializeModel model, ConsolePrinter console, DirectoryInformation information)
        {
            console.PrintFilterAll(information, commands.Arguments[1]);
            ConsolePrinter.WriteLine("Для возврата нажмите любую клавишу");
            Console.ReadKey(true);
        }
    }
}
