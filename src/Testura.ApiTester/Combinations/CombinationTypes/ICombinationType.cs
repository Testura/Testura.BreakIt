using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testura.ApiTester.Combinations.CombinationTypes
{
    public interface ICombinationType
    {
        Combination[] GetCombinations(string name, Type type, object defaultValue);
    }
}
