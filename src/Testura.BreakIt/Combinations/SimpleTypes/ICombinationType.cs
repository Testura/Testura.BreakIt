namespace Testura.BreakIt.Combinations.SimpleTypes
{
    public interface ICombinationType
    {
        Combination[] GetCombinations(string memberPath);
    }
}
