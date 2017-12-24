using System;

namespace Testura.FunkyValueTester.Combinations.SimpleTypes
{
    public interface ICombinationType
    {
        Combination[] GetCombinations(string name, Type type, object defaultValue);
    }
}
