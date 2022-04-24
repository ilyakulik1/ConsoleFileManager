using ConsoleFileManager.Commands;
using ConsoleFileManager.Commands.Interface;

namespace ConsoleFileManager.Commands.Factory
{
    public class CommandFactory
    {
        public ICommand CreateCommand(string input)
        {
            return input switch
            {
                "mkdir" => new MkDirCommand(),
                "info" => new InfoCommand(),
                "cmdlist" => new CmdlistCommand(),
                "cd" => new CdCommand(),
                "search" => new SearchCommand(),
                "searchall" => new SearchAllCommand(),
                "clear" => new ClearCommand(),
                "back" => new BackCommand(),
                "delete" => new DeleteCommand(),
                "copy" => new CopyCommand(),
                "move" => new MoveCommand(),
                "start" => new StartCommand(),
                "add_a" => new SetAtributeCommand(),
                "del_a" => new RemoveAtributeCommand(),
                "newfile" => new NewFileCommand(),
                _ => new UnknownCommand()
            };
        }
    }
}
