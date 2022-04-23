using ConsoleFileManager.Information;
using ConsoleFileManager.Serialization;
using ConsoleFileManager.ConsoleLogger;
using System;
using ConsoleFileManager.Commands.Interface;
using ConsoleFileManager.Commands.Factory;

namespace ConsoleFileManager
{
    public class Program
    {
        static void Main()
        {
            XmlSerialize serialize = XmlSerialize.GetInstance();
            SerializeModel model = serialize.Deserialize();
            ConsolePrinter console = new ConsolePrinter(model);
            var files = new DirectoryInformation(model);
            console.PrintList(files);
            console.PrintDirectoryInfo(files);
            Console.WriteLine("\nДобро пожаловать в Simple Console File Manager.\nЧтобы посмотреть список команд введите cmdlist");
            console.WriteCommandField();

            //Цикл для ввода команд
            while (true)
            {
                var commandArguments = new CommandArgumentsGetter(Console.ReadLine());
                CommandFactory _commandFactory = new CommandFactory();
                ICommand command = _commandFactory.CreateCommand(commandArguments.Arguments[0]);
                command.Execute(commandArguments, model, console, files);
                console.WriteCommandField();
            }
        }
    }
}