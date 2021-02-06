using System;

namespace Blurr
{
    public class Util
    {
        private Util() { }

        /// <summary>
        /// Create a console log
        /// </summary>
        /// <param name="logType">Type of console message</param>
        /// <param name="source">The source of the message</param>
        /// <param name="message">The message</param>
        public static void Log(LogType logType, string source, string message)
        {
            DateTime time = DateTime.Now;

            switch (logType)
            {
                case LogType.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogType.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogType.Info:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                case LogType.Verbose:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case LogType.Important:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
            }

            Console.WriteLine($"{time,20} - [{logType,8}] {source,10}: {message}");
            Console.ResetColor();
        }
    }

    /// <summary>
    /// The different log types
    /// </summary>
    public enum LogType
    {
        Warning, Error, Info, Verbose, Important
    }
}
