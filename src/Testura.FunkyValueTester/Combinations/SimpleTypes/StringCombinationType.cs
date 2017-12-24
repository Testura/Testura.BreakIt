namespace Testura.FunkyValueTester.Combinations.SimpleTypes
{
    public class StringCombinationType : ICombinationType
    {
        public Combination[] GetCombinations(string memberPath)
        {
            return new[] { new Combination(memberPath, string.Empty), new Combination(memberPath, null) };
        }
    }
}
