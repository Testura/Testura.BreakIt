namespace Testura.ApiTester.Combinations.Loggers.Formatters
{
    public interface ILogFormatter
    {
        string GetFormat(CombinationResult result);
    }
}
