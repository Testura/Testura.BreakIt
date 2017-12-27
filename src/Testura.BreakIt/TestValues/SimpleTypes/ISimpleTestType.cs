#pragma warning disable 1591
namespace Testura.BreakIt.TestValues.SimpleTypes
{
    public interface ISimpleTestType
    {
        /// <summary>
        /// Get the test values for a specific type.
        /// </summary>
        /// <param name="memberPath">Member path to the specifc test value</param>
        /// <returns>An array of all test values. </returns>
        TestValue[] GetTestValue(string memberPath);
    }
}
