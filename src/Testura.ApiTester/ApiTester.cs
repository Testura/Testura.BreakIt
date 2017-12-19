using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Testura.ApiTester.Combinations;
using Testura.ApiTester.DefaultValues;

namespace Testura.ApiTester
{
    public class ApiTester
    {
        private readonly CombinationLogger _logger;
        private readonly ICombinationFactory _combinationFactory;

        public ApiTester(CombinationLogger combinationLogger, Func<CombinationFactoryOptions, CombinationFactoryOptions> options = null)
        {
            _logger = combinationLogger;
            _combinationFactory = new CombinationFactory(options?.Invoke(new CombinationFactoryOptions()) ?? new CombinationFactoryOptions());
        }

        public ApiTester(CombinationLogger combinationLogger, ICombinationFactory combinationFactory)
        {
            _logger = combinationLogger;
            _combinationFactory = combinationFactory;
        }

        public void Execute(object testObject, string methodName, IList<object> defaultValues)
        {
            Execute(testObject, testObject.GetType().GetMethod(methodName), defaultValues);
        }

        public void Execute(object testObject, MethodInfo method, IList<object> defaultValues)
        {
            var paramenters = method.GetParameters();
            if (defaultValues.Count != paramenters.Length)
            {
                throw new Exception("Wrong number of default values");
            }

            var createdDefaultValues = new List<DefaultValue>();
            for (int n = 0; n < paramenters.Length; n++)
            {
                createdDefaultValues.Add(new DefaultValue(paramenters[n].Name, defaultValues[n]));
            }

            var defaultObjectValues = BuildDefaultObject(method.GetParameters(), createdDefaultValues);
            for (int n = 0; n < defaultObjectValues.Count; n++)
            {
                TestCombinations(testObject, method, defaultObjectValues, n);
            }
        }

        private void TestCombinations(object testObject, MethodInfo method, List<DefaultValueParameter> values, int currentIndex)
        {
            var type = values[currentIndex].ParameterInfo.ParameterType;
            var combinations = _combinationFactory.GetCombinations(values[currentIndex].ParameterInfo.Name, type, values[currentIndex].DefaultValue);
            var list = new object[values.Count];
            values.Select(v => v.DefaultValue).ToList().CopyTo(list);
            foreach (var combination in combinations)
            {
                list[currentIndex] = combination.Value;
                Exception invokeException = null;
                try
                {
                    method.Invoke(testObject, list);
                }
                catch (Exception ex)
                {
                    invokeException = ex;
                }

                _logger.Log(combination, invokeException);
            }
        }

        private List<DefaultValueParameter> BuildDefaultObject(ParameterInfo[] parameterInfos, IList<DefaultValue> defaultValues)
        {
            var values = new List<DefaultValueParameter>();
            foreach (var parameterInfo in parameterInfos)
            {
                var defaultValue = defaultValues.FirstOrDefault(d => d.Name.Equals(parameterInfo.Name, StringComparison.OrdinalIgnoreCase));
                if (defaultValue != null)
                {
                    values.Add(new DefaultValueParameter(parameterInfo, defaultValue.Value));
                }
                else
                {
                    values.Add(new DefaultValueParameter(parameterInfo, Activator.CreateInstance(parameterInfo.ParameterType)));
                }
            }

            return values;
        }
    }
}
