using System.Collections.Generic;
using System;
using ConsoleFileManager.ConsoleLogger;

namespace ConsoleFileManager.Information
{
    public class CommandArgumentsGetter
    {
        private List<string> _arguments = new List<string>(4);
        public List<string> Arguments { get { return _arguments; } }
        public string Command { get; }
        public string FirstArgument { get; }
        public string SecondArgument { get; }
        public CommandArgumentsGetter(string input)
        {
            try
            {
                string[] args = input.Split('\"');
                foreach (var item in args)
                {
                    if (item != " " && item != "")
                    {
                        _arguments.Add(item.Trim(' '));
                    }
                }
                if (Arguments.Count < 3)
                {
                    Arguments.Add("\"noargument\"");
                    Arguments.Add("\"noargument\"");
                }
            }
            catch (Exception)
            {
                ConsolePrinter.LogError();
            }
        }
    }
}
