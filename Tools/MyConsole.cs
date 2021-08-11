using System;
using System.Collections.Generic;
using System.Text;

namespace HeadFirst_DesignPatterns.Tools
{
    class MyConsole
    {
        public static void WriteLine(ConsoleColor color, string text)
        {
            WriteLine(text, color);
        }
        public static void WriteLine(string text = "", ConsoleColor color = ConsoleColor.White, int delay = 0, bool clearScreenAfter = false)
        {
            //Quick way to print message with custom color (or suppress message)
            if(color != ConsoleColor.White)
            {
                Console.ForegroundColor = color;
                Console.WriteLine(text);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.WriteLine(text);
            }
            if(delay > 0)
            {
                System.Threading.Thread.Sleep(delay);
            }
            if (clearScreenAfter)
            {
                Console.Clear();
            }
        }

        public static void PrintThenClear(string message, bool clear = true)
        {
            Console.WriteLine(message);
            System.Threading.Thread.Sleep(message.Length * 50 + 500); //20 characters per second = 50ms per character (and added 1/2 second delay)
            if (clear)
            {
                Console.Clear();
            }
        }
    }
}
