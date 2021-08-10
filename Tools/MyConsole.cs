using System;
using System.Collections.Generic;
using System.Text;

namespace HeadFirst_DesignPatterns.Tools
{
    class MyConsole
    {
        public static void WriteLine(ConsoleColor color, string text)
        {
            //Quick way to print message with custom color (or suppress message)
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
