using System;

namespace Testura.BreakIt.TestValues.TestValueLoggers
{
    public class ConsoleTestValueLogger : TestValueLogger
    {
        public override void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
