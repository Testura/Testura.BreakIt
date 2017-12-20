using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Testura.ApiTester.Combinations;
using Testura.ApiTester.Combinations.Loggers;
using Testura.ApiTester.DefaultValues;

namespace Testura.ApiTester
{
    public class ApiTester
    {
        private readonly CombinationLogger _logger;
        private readonly ApiTesterOptions _options;
        private readonly ICombinationFactory _combinationFactory;

        public ApiTester(CombinationLogger combinationLogger, Func<ApiTesterOptions, ApiTesterOptions> options = null)
        {
            _logger = combinationLogger;
            _options = options?.Invoke(new ApiTesterOptions()) ?? new ApiTesterOptions();
            _combinationFactory = new CombinationFactory(_options.ExcludeList);
        }

        public ApiTester(CombinationLogger combinationLogger, ICombinationFactory combinationFactory, Func<ApiTesterOptions, ApiTesterOptions> options = null)
            : this(combinationLogger, options)
        {
            _combinationFactory = combinationFactory;
        }

        public IList<CombinationResult> Execute(object testObject, string methodName, IList<object> defaultValues)
        {
            return Execute(testObject, testObject.GetType().GetMethod(methodName), defaultValues);
        }

        public IList<CombinationResult> Execute(object testObject, MethodInfo method, IList<object> defaultValues)
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
                results.AddRange(TestCombinations(testObject, method, defaultObjectValues, n));
            }

            return results;
        }

        private List<CombinationResult> TestCombinations(object testObject, MethodInfo method, List<DefaultValueParameter> values, int currentIndex)
        {
            var results = new List<CombinationResult>();
            var type = values[currentIndex].ParameterInfo.ParameterType;
            var combinations = _combinationFactory.GetCombinations(values[currentIndex].ParameterInfo.Name, type, values[currentIndex].DefaultValue);
            var list = new object[values.Count];
            values.Select(v => v.DefaultValue).ToList().CopyTo(list);
            foreach (var combination in combinations)
            {
                list[currentIndex] = combination.Value;
                Exception invokeException = null;
                object returnValue = null;
                try
                {
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
                    IsValidationOk = _options.Validation?.Invoke(returnValue, invokeException),
                    ReturnValue = returnValue
                };

                _logger.Log(result);
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
