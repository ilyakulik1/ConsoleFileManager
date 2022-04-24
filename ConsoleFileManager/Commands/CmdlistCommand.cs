using ConsoleFileManager.Commands.Interface;
using ConsoleFileManager.ConsoleLogger;
using ConsoleFileManager.Information;
using ConsoleFileManager.Serialization;
using System;
using System.Text;

namespace ConsoleFileManager.Commands
{
    public class CmdlistCommand : ICommand
    {
        public void Execute(CommandArgumentsGetter commands, SerializeModel model, ConsolePrinter console, DirectoryInformation information)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("cd - переход к следующему каталогу");
            builder.AppendLine("back - возврат к предыдущему каталогу");
            builder.AppendLine("mkdir - создание нового каталога");
            builder.AppendLine("delete - удаление");
            builder.AppendLine("copy - копирование");
            builder.AppendLine("move - перемещение\\переименование");
            builder.AppendLine("clear - очистка консоли");
            builder.AppendLine("info - информация о файле или каталоге");
            builder.AppendLine("start - запуск файла");
            builder.AppendLine("search - поиск файла в каталоге");
            builder.AppendLine("searchall - поиск файла в каталоге и во вложенных папках");
            builder.AppendLine("add_a - установить аттрибут ( Hidden, ReadOnly, Archive, System )");
            builder.AppendLine("del_a - удалить аттрибут ( Hidden, ReadOnly, Archive, System )");
            ConsolePrinter.WriteLine(builder.ToString());
        }
    }
}
