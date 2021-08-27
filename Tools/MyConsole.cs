using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Tools
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
            System.Threading.Thread.Sleep(message.Length * 50 + 500); //average person can read 20 characters per second = 50ms per character (and added 1/2 second delay)
            if (clear)
            {
                Console.Clear();
            }
        }

        public static string[] StackTrace_FunctionsOnly(string separator = "->")
        {
            //string stack = Environment.StackTrace;
            var st = new StackTrace();
            Stack<string> functionStack = new Stack<string>();
            for (int i = 1; i < st.FrameCount; i++)
            {
                //skip frame 0 since that's this function and we don't need it for reporting 
                var sf = st.GetFrame(i);
                functionStack.Push(sf.GetMethod().Name);

            }

            bool firstLine = true;
            string[] results = functionStack.ToArray();
            while (functionStack.Count > 0)
            {
                if (!firstLine)
                {
                    Console.Write(separator);
                }
                Console.Write($"{functionStack.Pop()}");
                firstLine = false;
            }
            Console.WriteLine("");
            return results;
        }
    }
}
