using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
