using System.Collections.Generic;

namespace Testura.BreakIt.Combinations.CombinationLoggers
{
    public class MemoryCombinationLogger : CombinationLogger
    {
        public MemoryCombinationLogger()
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
