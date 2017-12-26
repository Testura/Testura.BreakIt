namespace Testura.BreakIt.TestValues.TestValueLoggers.Formatters
{
    public interface ILogFormatter
    {
        string GetFormat(TestValueResult result);
    }
}
