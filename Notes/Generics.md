===============================================================================================================

C# GENERICS

https://app.pluralsight.com/library/courses/csharp-generics/table-of-contents

===============================================================================================================

CASTING TO GENERIC INTERFACES

https://stackoverflow.com/questions/222403/casting-an-object-to-a-generic-interface

Casting a variable using a Type variable
https://stackoverflow.com/questions/972636/casting-a-variable-using-a-type-variable
https://stackoverflow.com/questions/23714059/cast-with-typeof
https://stackoverflow.com/questions/464765/is-possible-to-cast-a-variable-to-a-type-stored-in-another-variable

Compare objects based on IComparable<T> with unknown T
https://www.pcreview.co.uk/threads/compare-objects-based-on-icomparable-t-with-unknown-t.3942000/

***************************************************************************************

One possible way using dynamic & implelemting IConvertible

	Casting to IComparable<T> when you can check that an instance of a type T 
	is actually an implementation of IComparable<T> is difficult because you 
	can usually work out T using typeof(instance) and store it as a Type var
	bay you cannot use that var to cast the instance like in the following.

	var comparable = (IComparable<varType>)instance;

	The compiler complains that varTyep is used as a type but it is a variable
	instead.

	One way of getting around this is by the following process.

	1- 
	First have a way to work out if an istance of an object is actually an
	implementation of IComparable<T>. If it is it should be that T is also 
	the type of the instance that is if instance is of tyep SomeClass and 
	inplements IComparable<T> then T ought to be SomeClass.

	2-
	Once you know that instance is SomeClass and implements IComparable<SomeClass>
	then you know that once cast to IComparable<SomeClass> it is all right to
	invoke the IComparable<SomeClass>.CompareTo(SomeClass x) on it.
	You can do this using a dynamic variable as below.
			
	SomeClass toCompare = new SomeClass(...)
	dynamic comparable = Convert.ChangeType(argument, typeof(IComparable<>));
	var result = comparable.CompareTo(toCompare)
 

***************************************************************************************

===============================================================================================================

IComparable & IComparable<T> & IComparer & IEquatable<T>

Differences
https://stackoverflow.com/questions/34242746/difference-between-icomparable-and-icomparablet-in-this-search-method
https://stackoverflow.com/questions/7301277/icomparable-and-icomparablet

Should you implement both or either?
https://stackoverflow.com/questions/7301277/icomparable-and-icomparablet

How to correctly implement IComparable<T> with consideration of inheritance
http://www.codinghelmet.com/?path=howto/implement-icomparable-t

difference between IComparable and IComparer [duplicate]
https://stackoverflow.com/questions/5980780/difference-between-icomparable-and-icomparer

When to use IComparable<T> Vs. IComparer<T>
https://stackoverflow.com/questions/538096/when-to-use-icomparablet-vs-icomparert

What is the difference between IEqualityComparer<T> and IEquatable<T>?
https://stackoverflow.com/questions/9316918/what-is-the-difference-between-iequalitycomparert-and-iequatablet

===============================================================================================================

TESTING IF A GENERIC INTERFACE IS IMPLEMENTED

===============================================================================================================

IConvertible

Implementing IConvertible interface
https://msdn.microsoft.com/en-us/library/system.iconvertible(v=vs.110).aspx
https://dotnetcodr.com/2015/04/22/type-conversion-example-in-c-net-using-the-iconvertible-interface/
https://stackoverflow.com/questions/40300537/implementing-iconvertible-interface

===============================================================================================================