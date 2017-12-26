using System.Collections.Generic;
using System.Linq;
using Testura.BreakIt.TestValues.TestValueLoggers.Formatters;

namespace Testura.BreakIt.TestValues.TestValueLoggers
{
    public abstract class TestValueLogger
    {
        private readonly List<ILogFormatter> _logFormatters;

        protected TestValueLogger()
        {
            _logFormatters = new List<ILogFormatter>
            {
                new TestValueFormatter(),
                new ValidationFormatter(),
                new ReturnValueFormatter(),
                new ExceptionValueFormatter()
            };
        }

        protected TestValueLogger(List<ILogFormatter> logFormatters)
        {
            _logFormatters = new List<ILogFormatter>(logFormatters);
        }

        public void Log(TestValueResult testValueResult)
        {
            Log(string.Join(", ", _logFormatters.Select(l => l.GetFormat(testValueResult)).ToArray()));
        }

        public abstract void Log(string message);
    }
}