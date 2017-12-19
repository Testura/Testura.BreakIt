using System.Reflection;

namespace Testura.ApiTester.DefaultValues
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
