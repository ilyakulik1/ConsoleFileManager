using ConsoleFileManager.Serialization;
using System;
using System.Diagnostics;
using System.IO;
using ConsoleFileManager.ConsoleLogger;
using ConsoleFileManager.Information;
using ConsoleFileManager.Commands.Interface;

namespace ConsoleFileManager.Commands
{
    public class StartCommand : ICommand
    {
        public void Execute(CommandArgumentsGetter commands, SerializeModel model, ConsolePrinter console, DirectoryInformation information)
        {
            var file = information.GetFileInfoFromArgument(commands.Arguments[1]);
            if (file != null)
            {
                var process = new Process();
                process.StartInfo.FileName = Path.Combine(model.FilePath, file.Name);
                process.StartInfo.UseShellExecute = true;
                try
                {
                    process.Start();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    process.Close();
                }
                return;
            }
            ConsolePrinter.LogNotFound(commands.Arguments[1]);
        }
    }
}
