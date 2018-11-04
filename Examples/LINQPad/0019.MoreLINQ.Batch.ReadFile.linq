<Query Kind="Program">
  <Reference Relative="Binaries\MoreLinq.dll">C:\GitHub\Loggers\Examples\LINQPad\Binaries\MoreLinq.dll</Reference>
  <Namespace>MoreLinq.Extensions</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Linq</Namespace>
</Query>

// C# Program
// ITs like you are in the body of the classic static class Program of a console app

// Refs 
// on LINQ
// https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable?view=netframework-4.7.2

// on using statements in LINQPad
// https://forum.linqpad.net/discussion/1020/using-namespace-statements-in-c-query-file
// https://spottedmahn.wordpress.com/2010/03/16/linqpad-using-statement-problem/

// Info
// To see the properties of a LINQPad query right-click and select References and Properties.

// Here we read a file with a repeated set structure of 7 lines of text followed by a single 
// blank line and convert the contents of thefile into an anonymous object that may be used 
// for further processing into a LINQ pipeline. In this example the MoreLINQ Batch is employed
// to lump the IEnumerable<string> emerging from the File.ReadAllLines into a 
// IEnumerable<IEnumerable<string>> and then to <IEnumerable<string[]> that is eventually 
// projected into an anonymous object. Notice that converting the IEnumerable<string> to string[]
// provides the advantage of making the content of each line availble by indexing on the array.

void Main() { 

	File.ReadAllLines(@"C:\GitHub\Loggers\Examples\LINQPad\Assets\2018.11.04.questions.txt")
	.Batch(7, b => b.ToArray())
	.Select(b => new {
		Key = b[0],
		Question = b[1],
		CorrectAnswer = b[2],
		WrongAnswer1 = b[3],
		WrongAnswer2 = b[4],
		WrongAnswer3 = b[5],
	})
	.Dump(); 
}