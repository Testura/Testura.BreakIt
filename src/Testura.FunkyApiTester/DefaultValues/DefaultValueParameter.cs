using System.Reflection;

namespace Testura.FunkyApiTester.DefaultValues
{
    internal class DefaultValueParameter
    {
        public DefaultValueParameter(ParameterInfo parameterInfo, object defaultValue)
        {
            ParameterInfo = parameterInfo;
            DefaultValue = defaultValue;
        }

        public ParameterInfo ParameterInfo { get; set; }

        public object DefaultValue { get; set; }
    }
}
