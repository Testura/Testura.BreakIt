namespace Testura.FunkyValueTester.Combinations.SimpleTypes
{
    public class DoubleCombinationType : ICombinationType
    {
        public Combination[] GetCombinations(string memberPath)
        {
            return new[] { new Combination(memberPath, double.MaxValue), new Combination(memberPath, double.MinValue) };
        }
    }
}
