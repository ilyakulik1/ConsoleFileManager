using ConsoleFileManager.Serialization;
using System.IO;
using System;
using ConsoleFileManager.ConsoleLogger;
using ConsoleFileManager.Information;
using ConsoleFileManager.Commands.Interface;

namespace ConsoleFileManager.Commands
{
    public class MoveCommand : ICommand
    {
        public void Execute(CommandArgumentsGetter commands, SerializeModel model, ConsolePrinter console, DirectoryInformation information)
        {
            try
            {
                var dirs = information.GetDirectoyInfoFromArgument(commands.Arguments[1]);
                if (dirs != null)
                {
                    Directory.Move(Path.Combine(model.FilePath, dirs.Name), Path.Combine(model.FilePath, commands.Arguments[2]));
                    console.PrintList(information);
                    console.PrintDirectoryInfo(information);
                    return;
                }
                var files = information.GetFileInfoFromArgument(commands.Arguments[1]);
                if (files != null)
                {
                    try
                    {
                        File.Move(Path.Combine(model.FilePath, files.Name), Path.Combine(model.FilePath, commands.Arguments[2]), true);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    console.PrintList(information);
                    console.PrintDirectoryInfo(information);
                    return;
                }
                ConsolePrinter.LogNotFound(commands.Arguments[1]); 
            }
            catch (Exception ex)
            {
                ConsolePrinter.LogError(ex.Message);
            }
        }
    }
}
