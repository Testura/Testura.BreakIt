using System;

namespace Testura.ApiTester.Combinations.CombinationTypes
{
    public interface ICombinationType
    {
        Combination[] GetCombinations(string name, Type type, object defaultValue);
    }
}
