namespace Testura.FunkyApiTester.Combinations.Loggers.Formatters
{
    public interface ILogFormatter
    {
        string GetFormat(CombinationResult result);
    }
}
