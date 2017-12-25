using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Testura.BreakIt.Combinations;
using Testura.BreakIt.Combinations.CombinationLoggers;
using Testura.BreakIt.DefaultValues;

namespace Testura.BreakIt
{
    public class BreakIt
    {
        private readonly CombinationLogger _logger;
        private readonly ICombinationFactory _combinationFactory;

        public BreakIt()
        {
            _combinationFactory = new CombinationFactory();
        }

        public BreakIt(CombinationLogger combinationLogger = null, ICombinationFactory combinationFactory = null)
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

            var createdDefaultValues = paramenters.Select(
                (parameter, index) => new DefaultValue(parameter.Name, defaultValues[index])).ToList();
            var defaultObjectValues = BuildDefaultObject(method.GetParameters(), createdDefaultValues);

            var results = new List<CombinationResult>();
            for (int i = 0; i < defaultObjectValues.Count; i++)
            {
                results.AddRange(TestCombinations(testObject, method, defaultObjectValues, i, options));
            }

            return results;
        }

        private IEnumerable<CombinationResult> TestCombinations(object testObject, MethodInfo method, IList<DefaultValueParameter> values, int currentIndex, TesterOptions options)
        {
            var results = new List<CombinationResult>();
            var type = values[currentIndex].ParameterInfo.ParameterType;
            var combinations = _combinationFactory.GetCombinations(values[currentIndex].ParameterInfo.Name, type, values[currentIndex].DefaultValue, options?.ExcludeList);
            var list = new object[values.Count];
            values.Select(v => v.DefaultValue).ToList().CopyTo(list);
            foreach (var combination in combinations)
            {
                results.Add(InvokeMethood(testObject, method, currentIndex, options, list, combination));
            }

            return results;
        }

        private CombinationResult InvokeMethood(object testObject, MethodInfo method, int currentIndex, TesterOptions options, object[] list, Combination combination)
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
                MemberPath = combination.MemberPath,
                Exception = invokeException,
                TestingValue = combination,
                IsSuccess = options?.Validation?.Invoke(returnValue, invokeException),
                ReturnValue = returnValue
            };

            _logger?.Log(result);
            return result;
        }

        private IList<DefaultValueParameter> BuildDefaultObject(ParameterInfo[] parameterInfos, IList<DefaultValue> defaultValues)
        {
            var values = new List<DefaultValueParameter>();
            foreach (var parameterInfo in parameterInfos)
            {
                var defaultValue = defaultValues.FirstOrDefault(d => d.Name.Equals(parameterInfo.Name, StringComparison.OrdinalIgnoreCase));
                values.Add(new DefaultValueParameter(parameterInfo, defaultValue?.Value));
            }

            return values;
        }
    }
}
