using System;

namespace Testura.ApiTester.Combinations.SimpleTypes
{
    public interface ICombinationType
    {
        Combination[] GetCombinations(string name, Type type, object defaultValue);
    }
}
