![Testura Logo](http://testura.net/Content/Images/logo2.png)

BreakIt is a simple framework to test your method or api by sending different test values. The goal is to "break" the method or api by sending extreme values, boundary or funky data.

Technically the framework will look at your method and parameters and map it to different test values that it use at execution.

Currently we support: 

- `bool`
- `int`
- `double`
- `float`
- `decimal`
- `short`
- `string?`
- `enum`
- `IList`
- `Dictionary` (and `IDictionary`)


# Install

## NuGet [![NuGet Status](https://img.shields.io/nuget/v/Testura.Android.svg?style=flat)](https://www.nuget.org/packages/Testura.Android)

[https://www.nuget.org/packages/Testura.Android](https://www.nuget.org/packages/Testura.Android)
    
    PM> Install-Package Testura.Android
   

## Usage

### Example 1 - Simple method

Method or api to test: 

```c#
using System;
using System.Collections.Generic;

namespace Testura.BreakIt.Tests.Help
{
    public class MyApi
    {
        public void CallApi(int id, string name) { .. }
    }
}
```
Execute the test:

```c#
var myApi = new MyApi(); 
var breakIt = new BreakItTester();

var result = breakIt.Execute(myApi, nameof(myApi.CallApi), new List<object> { 1, "testName });
```

After exection we get this result: 

```c#
Testing parameter id => 2147483647, Validation => No validation done, Return value => null or no return value, Exception => No exception
Testing parameter id => -2147483648, Validation => No validation done, Return value => null or no return value, Exception => No exception
Testing parameter name => empty, Validation => No validation done, Return value => null or no return value, Exception => No exception
Testing parameter name => null, Validation => No validation done, Return value => null or no return value, Exception => No exception
```

It's very important that you provide default parameters that is valid and match the method (`new List<object> { 1, "testName }` in the example). 


### Example 2 - More advanced case

Method or api to test: 

```c#
using System;
using System.Collections.Generic;

namespace Testura.BreakIt.Tests.Help
{
    public class MyApi
    {
        public void CallApiComplex(int id, List<SomeComplexType> list, Dictionary<string, SomeComplexType> dictionary, SomeEnum someEnum) { .. }
    }
}
```

Execute the test:

```c#
var myApi = new MyApi(); 
var breakIt = new BreakItTester();

var defaultValue = new SomeComplexType { Name = "Test", Number = 3 };
var result breakIt.Execute(myApi, nameof(myApi.CallApiComplex), new List<object> { 1, new List<SomeComplexType> { defaultValue }, new Dictionary<string, SomeComplexType> { ["Key"] = defaultValue }, MyApi.SomeEnum.FirstValue });
```

After exection we get this result: 

```c#
Testing parameter id => 2147483647, Validation => No validation done, Return value => null or no return value, Exception => No exception
Testing parameter id => -2147483648, Validation => No validation done, Return value => null or no return value, Exception => No exception
Testing parameter list[0].Name => empty, Validation => No validation done, Return value => null or no return value, Exception => No exception
Testing parameter list[0].Name => null, Validation => No validation done, Return value => null or no return value, Exception => No exception
Testing parameter list[0].Number => 2147483647, Validation => No validation done, Return value => null or no return value, Exception => No exception
Testing parameter list[0].Number => -2147483648, Validation => No validation done, Return value => null or no return value, Exception => No exception
Testing parameter dictionary[Key].Name => empty, Validation => No validation done, Return value => null or no return value, Exception => No exception
Testing parameter dictionary[Key].Name => null, Validation => No validation done, Return value => null or no return value, Exception => No exception
Testing parameter dictionary[Key].Number => 2147483647, Validation => No validation done, Return value => null or no return value, Exception => No exception
Testing parameter dictionary[Key].Number => -2147483648, Validation => No validation done, Return value => null or no return value, Exception => No exception
Testing parameter someEnum => FirstValue, Validation => No validation done, Return value => null or no return value, Exception => No exception
Testing parameter someEnum => SecondValue, Validation => No validation done, Return value => null or no return value, Exception => No exception
```

As you can see it go through both list and dictionaries as long as they contain a default value.

## Options 

It is possible to provide a validation method or exclude types/members/parameters through the option object.

Example for validation (check if method throws internal server error): 

```c#
private bool Validation(TestValue value, object returnValues, Exception exception)
{
	return !exception.Message.Contains("internal server error");
}
```

```c#
var breakIt = new BreakItTester();
breakIt.Execute(someApi, nameof(someApi.SomeMethod), new List<object> { .. }, new TesterOptions { Validation = Validation });
```

Example to exclude all members/parameters that contains "NotUsed" in it's name: 

```c#
private bool Exclude(string memberPath, Type type)
{
	return memberPath.Contains("NotUsed");
}
```

```c#
var options = new TesterOptions();
options.AddExclude(Exclude);

var breakIt = new BreakItTester();
breakIt.Execute(someApi, nameof(someApi.SomeMethod), new List<object> { .. }, option);
```

## Extend with new types

It is possible to extend the framework with your own test types if you want to handle them in a specific way. 

First you implement the `ISimpleTestType` interface: 

```c#
public interface ISimpleTestType
{
	/// <summary>
	/// Get the test values for a specific type.
	/// </summary>
	/// <param name="memberPath">Member path to the specifc test value</param>
	/// <returns>An array of all test values. </returns>
	TestValue[] GetTestValue(string memberPath);
}
```

Then you add it the TestValueFactory when creating a BreakItTester instance> 

```c#
var dictionary = new Dictionary<Type, ISimpleTestType>
{
	[yourType] = new YourImplementedISimpleTestType()
};

var breakIt = new BreakItTester(testValueFactory: new TestValueFactory(dictionary));
```

## Logging 

BreakIt returns a result object but sometimes it's also good to log all values. Currently we have three diffrent loggers: 

- `ConsoleTestValueLogger`
- `FileTestValueLogger`
- `MemoryTestValueLogger`

And you simple provide them when creating a new BreakIt instance: 

```c#
var breakIt = new BreakItTester(new ConsoleTestValueLogger());
```

## License

This project is licensed under the MIT License. See the [LICENSE.md](LICENSE.md) file for details.

## Contact

Visit <a href="http://www.testura.net">www.testura.net</a>, twitter at @testuranet
