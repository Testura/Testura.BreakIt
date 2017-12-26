using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Testura.BreakIt.DefaultValues;
using Testura.BreakIt.TestValues;
using Testura.BreakIt.TestValues.TestValueLoggers;

namespace Testura.BreakIt
{
    public class BreakIt
    {
        private readonly TestValueLogger _logger;
        private readonly ITestValueFactory _combinationFactory;

        public BreakIt()
        {
            _combinationFactory = new TestValueFactory();
        }

        public BreakIt(TestValueLogger combinationLogger = null, ITestValueFactory combinationFactory = null)
            : this()
        {
            _logger = combinationLogger;
            _combinationFactory = combinationFactory ?? new TestValueFactory();
        }

        public IList<TestValueResult> Execute(object testObject, string methodName, IList<object> defaultValues, TesterOptions options = null)
        {
            return Execute(testObject, testObject.GetType().GetMethod(methodName), defaultValues, options);
        }

        public IList<TestValueResult> Execute(object testObject, MethodInfo method, IList<object> defaultValues, TesterOptions options = null)
        {
            var paramenters = method.GetParameters();
            if (defaultValues.Count != paramenters.Length)
            {
                throw new Exception("Each parameter must have a default value");
            }

            var createdDefaultValues = paramenters.Select(
                (parameter, index) => new DefaultValue(parameter.Name, defaultValues[index])).ToList();
            var defaultObjectValues = BuildDefaultObject(method.GetParameters(), createdDefaultValues);

            var results = new List<TestValueResult>();
            for (int i = 0; i < defaultObjectValues.Count; i++)
            {
                results.AddRange(TestCombinations(testObject, method, defaultObjectValues, i, options));
            }

            return results;
        }

        private IEnumerable<TestValueResult> TestCombinations(object testObject, MethodInfo method, IList<DefaultValueParameter> values, int currentIndex, TesterOptions options)
        {
            var results = new List<TestValueResult>();
            var type = values[currentIndex].ParameterInfo.ParameterType;
            var combinations = _combinationFactory.GetTestValues(values[currentIndex].ParameterInfo.Name, type, values[currentIndex].DefaultValue, options?.ExcludeList);
            var list = new object[values.Count];
            values.Select(v => v.DefaultValue).ToList().CopyTo(list);
            foreach (var combination in combinations)
            {
                results.Add(InvokeMethood(testObject, method, currentIndex, options, list, combination));
            }

            return results;
        }

        private TestValueResult InvokeMethood(object testObject, MethodInfo method, int currentIndex, TesterOptions options, object[] list, TestValue testValue)
        {
            list[currentIndex] = testValue.Value;
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

            var result = new TestValueResult
            {
                MemberPath = testValue.MemberPath,
                Exception = invokeException,
                TestingValue = testValue,
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
