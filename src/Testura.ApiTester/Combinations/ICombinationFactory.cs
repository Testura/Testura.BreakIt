using System;

namespace Testura.ApiTester.Combinations
{
    public interface ICombinationFactory
    {
        Combination[] GetCombinations(string name, Type type, object defaultValue);
    }
}