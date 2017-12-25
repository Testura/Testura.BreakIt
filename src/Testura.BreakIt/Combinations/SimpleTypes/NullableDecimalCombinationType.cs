namespace Testura.BreakIt.Combinations.SimpleTypes
{
    public class NullableDecimalCombinationType : ICombinationType
    {
        public Combination[] GetCombinations(string memberPath)
        {
            return new[] { new Combination(memberPath, decimal.MaxValue), new Combination(memberPath, decimal.MinValue), new Combination(memberPath, null) };
        }
    }
}
