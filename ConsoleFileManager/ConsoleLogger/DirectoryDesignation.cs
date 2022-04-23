using ConsoleFileManager.Serialization;
using System;

namespace ConsoleFileManager.ConsoleLogger
{
    public class DirectoryDesignation : IDesignation
    {
        public void Design(SerializeModel model)
        {
            Console.BackgroundColor = model.ColorFolder;
            Console.Write(" ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(" ");
        }
    }
}
