using ConsoleFileManager.Calculation;
using ConsoleFileManager.Commands.Interface;
using ConsoleFileManager.ConsoleLogger;
using ConsoleFileManager.Information;
using ConsoleFileManager.Serialization;
using System;
using System.IO;

namespace ConsoleFileManager.Commands
{
    public class InfoCommand : ICommand
    {
        public void Execute(CommandArgumentsGetter commands, SerializeModel model, ConsolePrinter console, DirectoryInformation information)
        {
            var dir = information.GetDirectoyInfoFromArgument(commands.Arguments[1]);
            if (dir != null)
            {
                ConsolePrinter.WriteLine();
                ConsolePrinter.WriteLine($"Имя: {dir.Name}");
                ConsolePrinter.WriteLine($"Атрибуты: {dir.Attributes}");
                ConsolePrinter.WriteLine($"Время создания: {dir.CreationTime}");
                ConsolePrinter.WriteLine($"Время последнго изменения: {dir.LastWriteTime}");
                ConsolePrinter.WriteLine($"Количество файлов: {new CountGetter(dir).GetFilesCount(dir)}");
                ConsolePrinter.WriteLine($"Количество файлов: {new CountGetter(dir).GetFolderCount(dir)}");
                ConsolePrinter.WriteLine($"Размер: {new SizeGetter(dir).GetFolderSize(dir)} байт");
                return;
            }
            var file = information.GetFileInfoFromArgument(commands.Arguments[1]);
            if (file != null)
            {
                ConsolePrinter.WriteLine();
                ConsolePrinter.WriteLine($"Имя: {file.Name}");
                ConsolePrinter.WriteLine($"Атрибуты: {file.Attributes}");
                ConsolePrinter.WriteLine($"Время создания: {file.CreationTime}");
                ConsolePrinter.WriteLine($"Время последнго изменения: {file.LastWriteTime}");
                ConsolePrinter.WriteLine($"Расширение: {file.Extension}");
                ConsolePrinter.WriteLine($"Только для чтения: {file.IsReadOnly}");
                ConsolePrinter.WriteLine($"Размер: {file.Length} байт");
                TextInfo(file);
                return;
            }

            ConsolePrinter.LogNotFound(commands.Arguments[1]);
        }

        private void TextInfo(FileInfo file)
        {
            if (file.Extension == ".txt")
            {
                using (var reader = new StreamReader(file.FullName))
                {
                    string text;
                    string[] array = null;
                    int lines = 0;
                    int words = 0;
                    int tabs = 0;
                    int symbols = 0;
                    int symbolsNotSpace = 0;
                    while ((text = reader.ReadLine()) != null)
                    {
                        symbols += text.Length;
                        if (text[0] == '\t')
                        {
                            tabs++;
                        }
                        array = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < array.Length; i++)
                        {
                            symbolsNotSpace += array[i].Length;
                        }
                        words += array.Length;
                        lines++;
                    }
                    ConsolePrinter.WriteLine($"Количество строк: {lines}");
                    ConsolePrinter.WriteLine($"Количество слов: {words}");
                    ConsolePrinter.WriteLine($"Количество абзацев: {tabs}");
                    ConsolePrinter.WriteLine($"Количество символов: {symbols}");
                    ConsolePrinter.WriteLine($"Количество символов без учета пробелов: {symbolsNotSpace}");
                }
            }
        }
    }
}
