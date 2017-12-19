using System;

namespace Testura.ApiTester.Combinations.Loggers
{
    public class ConsoleCombinationLogger : CombinationLogger
    {
        public override void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
