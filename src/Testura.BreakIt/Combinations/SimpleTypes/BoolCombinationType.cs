namespace Testura.BreakIt.Combinations.SimpleTypes
{
    public class BoolCombinationType : ICombinationType
    {
        public Combination[] GetCombinations(string memberPath)
        {
            return new[] { new Combination(memberPath, false), new Combination(memberPath, true) };
        }
    }
}
