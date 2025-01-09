using System;

namespace Fuyu.Common.IO
{
    public static class Terminal
    {
        private static readonly object _lock = new object();

        public static void WriteLine(string text)
        {
            var line = $"{text}\n";

            lock (_lock)
            {
                Console.Write(line);
                WriteToFile(line);
            }
        }

        public static void WriteLine(object o)
        {
            if (o == null)
            {
                throw new NullReferenceException();
            }

            WriteLine(o.ToString());
        }

        public static void WaitForInput()
        {
            // Console.ReadKey doesn't work in vscode buildin terminal
            Console.In.ReadLine();
        }

        private static void WriteToFile(string text)
        {
            VFS.WriteTextFile("./Fuyu/Logs/trace.log", text, true);
        }
    }
}