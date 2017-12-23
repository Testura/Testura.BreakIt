using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Testura.FunkyApiTester.Combinations;
using Testura.FunkyApiTester.Combinations.Loggers;
using Testura.FunkyApiTester.DefaultValues;

namespace Testura.FunkyApiTester
{
    public class FunkyApiTester
    {
        private readonly CombinationLogger _logger;
        private readonly ICombinationFactory _combinationFactory;

        public FunkyApiTester()
        {
            _combinationFactory = new CombinationFactory();
        }

        public FunkyApiTester(CombinationLogger combinationLogger = null, ICombinationFactory combinationFactory = null)
            : this()
        {
            _logger = combinationLogger;
            _combinationFactory = combinationFactory ?? new CombinationFactory();
        }

        public IList<CombinationResult> Execute(object testObject, string methodName, IList<object> defaultValues, TesterOptions options = null)
        {
            return Execute(testObject, testObject.GetType().GetMethod(methodName), defaultValues, options);
        }

        public IList<CombinationResult> Execute(object testObject, MethodInfo method, IList<object> defaultValues, TesterOptions options = null)
        {
            var paramenters = method.GetParameters();
            if (defaultValues.Count != paramenters.Length)
            {
                throw new Exception("Each parameter must have a default value");
            }

            var createdDefaultValues = new List<DefaultValue>();
            var results = new List<CombinationResult>();

            for (int n = 0; n < paramenters.Length; n++)
            {
                createdDefaultValues.Add(new DefaultValue(paramenters[n].Name, defaultValues[n]));
            }

            var defaultObjectValues = BuildDefaultObject(method.GetParameters(), createdDefaultValues);
            for (int n = 0; n < defaultObjectValues.Count; n++)
            {
                results.AddRange(TestCombinations(testObject, method, defaultObjectValues, n, options));
            }

            return results;
        }

        private List<CombinationResult> TestCombinations(object testObject, MethodInfo method, IList<DefaultValueParameter> values, int currentIndex, TesterOptions options)
        {
            var results = new List<CombinationResult>();
            var type = values[currentIndex].ParameterInfo.ParameterType;
            var combinations = _combinationFactory.GetCombinations(values[currentIndex].ParameterInfo.Name, type, values[currentIndex].DefaultValue, options?.ExcludeList);
            var list = new object[values.Count];
            values.Select(v => v.DefaultValue).ToList().CopyTo(list);
            foreach (var combination in combinations)
            {
                list[currentIndex] = combination.Value;
                Exception invokeException = null;
                object returnValue = null;
                try
                {
                    options?.SetUp?.Invoke(list);
                    returnValue = method.Invoke(testObject, list);
                }
                catch (Exception ex)
                {
                    invokeException = ex.InnerException;
                }

                var result = new CombinationResult
                {
                    Name = combination.Name,
                    Exception = invokeException,
                    TestingValue = combination,
                    ResultOk = options?.Validation?.Invoke(returnValue, invokeException),
                    ReturnValue = returnValue
                };

                _logger?.Log(result);
                results.Add(result);
            }

            return results;
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
