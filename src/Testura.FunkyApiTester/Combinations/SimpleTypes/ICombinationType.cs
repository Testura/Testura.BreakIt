using System;

namespace Testura.FunkyApiTester.Combinations.SimpleTypes
{
    public interface ICombinationType
    {
        Combination[] GetCombinations(string name, Type type, object defaultValue);
    }
}
