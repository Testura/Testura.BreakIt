using System.Collections.Generic;
using System.Linq;
using Testura.BreakIt.Combinations.CombinationLoggers.Formatters;

namespace Testura.BreakIt.Combinations.CombinationLoggers
{
    public abstract class CombinationLogger
    {
        private readonly List<ILogFormatter> _logFormatters;

        protected CombinationLogger()
        {
            _logFormatters = new List<ILogFormatter>
            {
                new TestValueFormatter(),
                new ValidationFormatter(),
                new ReturnValueFormatter(),
                new ExceptionValueFormatter()
            };
        }

        protected CombinationLogger(List<ILogFormatter> logFormatters)
        {
            _logFormatters = new List<ILogFormatter>(logFormatters);
        }

        public void Log(CombinationResult combinationResult)
        {
            Log(string.Join(", ", _logFormatters.Select(l => l.GetFormat(combinationResult)).ToArray()));
        }

        public abstract void Log(string message);
    }
}