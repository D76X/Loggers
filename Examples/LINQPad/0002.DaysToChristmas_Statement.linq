<Query Kind="Statements" />

// Select C# Statement

// this is an assignment statement
// notice that the ; is required now and the order of the statements is meaningful.
var daysToChristmas = (new DateTime(DateTime.Today.Year, 12, 25) - DateTime.Today).TotalDays;

// this is a statement that returns void and has printing a value to the console as its side effect.
Console.WriteLine(daysToChristmas);

// this is an alternative extension method used to print to the LINQPad console.
// the advantage of the Dump extension method fo the Object type is that it applies automatic formatting
// depending on the run-time type of the object.
daysToChristmas.Dump();

// here another example on an anonymous object.
var anonymousObject = new {Name="Davide", Surname="Spano"};
anonymousObject.Dump();