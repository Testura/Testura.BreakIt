using System;
using System.Collections.Generic;
using Testura.BreakIt.TestValues;

namespace Testura.BreakIt
{
    /// <summary>
    /// Optional testing options when executing a break it test.
    /// </summary>
    public class BreakItOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BreakItOptions"/> class.
        /// </summary>
        public BreakItOptions()
        {
            ExcludeList = new List<Func<string, Type, bool>>();
        }

        /// <summary>
        /// Gets or sets the validation function that is executed after every test value. The function
        /// accepts arguments for current test value, return value (null if method under test is void) and
        /// thrown exception. Return true if excetin is a success, false otherwise.
        /// </summary>
        public Func<TestValue, object, Exception, bool> Validation { get; set; }

        /// <summary>
        /// Gets or sets the setup method that is executed before every test value. The method accepts
        /// a list of all values inserted to the method under test.
        /// </summary>
        public Action<object[]> SetUp { get; set; }

        /// <summary>
        /// Gets the exclude list. The exclude item is a function that accepts member path and type as an
        /// argument. Return true to ignore member/type, otherwise false.
        /// </summary>
        internal IList<Func<string, Type, bool>> ExcludeList { get; }

        /// <summary>
        /// Add new functions to the exclude list.
        /// </summary>
        /// <param name="exclude">A function that accepts member path and type as an argument. Return true to ignore member/type, otherwise false.</param>
        public void AddExclude(Func<string, Type, bool> exclude)
        {
            if (exclude == null)
            {
                throw new ArgumentNullException(nameof(exclude));
            }

            ExcludeList.Add(exclude);
        }
    }
}
