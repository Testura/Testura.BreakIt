using System;
using System.Collections.Generic;

namespace Testura.BreakIt.TestValues
{
    /// <summary>
    /// Provides an interface to get dufferent test values
    /// </summary>
    public interface ITestValueFactory
    {
        /// <summary>
        /// Return all test values for a specific type.
        /// </summary>
        /// <param name="memberPath">The member path or name of parameter</param>
        /// <param name="type">The type of parameter/member.</param>
        /// <param name="defaultValue">The default value of parameter/member.</param>
        /// <param name="excludeList">A list with exclude functions that accepts member path, type and returns true if type should be ignored. </param>
        /// <returns>All possible test values.</returns>
        TestValue[] GetTestValues(string memberPath, Type type, object defaultValue, IList<Func<string, Type, bool>> excludeList = null);
    }
}