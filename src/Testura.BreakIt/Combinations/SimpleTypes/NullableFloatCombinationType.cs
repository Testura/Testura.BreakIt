namespace Testura.BreakIt.Combinations.SimpleTypes
{
    public class NullableFloatCombinationType : ICombinationType
    {
        public Combination[] GetCombinations(string memberPath)
        {
            return new[] { new Combination(memberPath, float.MaxValue), new Combination(memberPath, float.MinValue), new Combination(memberPath, null) };
        }
    }
}
