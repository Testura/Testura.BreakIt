using System;

namespace Testura.FunkyValueTester.Combinations.SimpleTypes
{
    public interface ICombinationType
    {
        Combination[] GetCombinations(string memberPath, Type type, object defaultValue);
    }
}
