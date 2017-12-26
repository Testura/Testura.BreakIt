using System.Collections.Generic;

namespace Testura.BreakIt.TestValues.TestValueLoggers
{
    public class MemoryTestValueLogger : TestValueLogger
    {
        public MemoryTestValueLogger()
        {
            LogLines = new List<string>();
        }

        public List<string> LogLines { get; }

        public override void Log(string message)
        {
            LogLines.Add(message);
        }
    }
}
