using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Testura.BreakIt.DefaultValues;
using Testura.BreakIt.TestValues;
using Testura.BreakIt.TestValues.TestValueLoggers;

namespace Testura.BreakIt
{
    /// <summary>
    /// Provides the functionallity to test multiple combinations of test values against
    /// a method/api.
    /// </summary>
    public class BreakItTester
    {
        private readonly TestValueLogger _logger;
        private readonly ITestValueFactory _combinationFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="BreakItTester"/> class.
        /// </summary>
        public BreakItTester()
        {
            _combinationFactory = new TestValueFactory();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BreakItTester"/> class.
        /// </summary>
        /// <param name="testValueLogger">Optional server to log each test value.</param>
        /// <param name="testValueFactory">Optional factory to create test value combinations.</param>
        public BreakItTester(TestValueLogger testValueLogger = null, ITestValueFactory testValueFactory = null)
            : this()
        {
            _logger = testValueLogger;
            _combinationFactory = testValueFactory ?? new TestValueFactory();
        }

        /// <summary>
        /// Execute multiple test against a method by invoking it with different test values.
        /// </summary>
        /// <param name="testObject">The base object to test</param>
        /// <param name="methodName">Name of the method to test.</param>
        /// <param name="defaultValues">A list of default values/argument to the method. Important that they match the invoked methods parameter list.</param>
        /// <param name="options">Optional options.</param>
        /// <returns>A list with the result of all test values.</returns>
        public IList<TestValueResult> Execute(object testObject, string methodName, IList<object> defaultValues, TesterOptions options = null)
        {
            return Execute(testObject, testObject.GetType().GetMethod(methodName), defaultValues, options);
        }

        /// <summary>
        /// Execute multiple test against a method by invoking it with different test valu
        /// </summary>
        /// <param name="testObject">The base object to test</param>
        /// <param name="method">The method to invoke.</param>
        /// <param name="defaultValues">A list of default values/argument to the method. Important that they match the invoked methods parameter list.</param>
        /// <param name="options">Optional options.</param>
        /// <returns>A list with the result of all test values.</returns>
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
                IsSuccess = options?.Validation?.Invoke(testValue, returnValue, invokeException),
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
