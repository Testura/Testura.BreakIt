using System;

namespace Testura.FunkyApiTester.Combinations.Loggers
{
    public class ConsoleCombinationLogger : CombinationLogger
    {
        public override void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
