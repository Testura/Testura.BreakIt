namespace Testura.FunkyValueTester.Combinations.Loggers.Formatters
{
    public interface ILogFormatter
    {
        string GetFormat(CombinationResult result);
    }
}
