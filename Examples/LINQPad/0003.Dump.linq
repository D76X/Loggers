<Query Kind="Statements" />

// Select C# Statement
// this is a statement that returns void and has printing a value to the console as its side effect.
Console.WriteLine(100);

// this is an alternative extension method used to print to the LINQPad console.
// the advantage of the Dump extension method fo the Object type is that it applies automatic formatting
// depending on the run-time type of the object.
100.Dump();

// here another example on an anonymous object.
var anonymousObject = new {Name="Davide", Surname="Spano"};
anonymousObject.Dump();