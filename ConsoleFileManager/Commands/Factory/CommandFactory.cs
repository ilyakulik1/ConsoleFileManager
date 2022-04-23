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
                "setattribute" => new SetAtributeCommand(),
                "removeattribute" => new RemoveAtributeCommand(),
                _ => new UnknownCommand()
            };
        }
    }
}
