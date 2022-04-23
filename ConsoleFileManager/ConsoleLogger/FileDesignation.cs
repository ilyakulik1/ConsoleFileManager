using ConsoleFileManager.Serialization;
using System;

namespace ConsoleFileManager.ConsoleLogger
{
    public class FileDesignation : IDesignation
    {
        public void Design(SerializeModel model)
        {
            Console.BackgroundColor = model.ColorFile;
            Console.Write(" ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(" ");
        }
    }
}
