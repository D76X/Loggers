<Query Kind="Statements">
  <Reference Relative="Binaries\MoreLinq.dll">C:\GitHub\Loggers\Examples\LINQPad\Binaries\MoreLinq.dll</Reference>
  <Namespace>MoreLinq.Extensions</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Linq</Namespace>
</Query>

// C# Statements
// Statements are evaluated by the compiler and return a result.

// Refs 
// on LINQ
// https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable?view=netframework-4.7.2

// on using statements in LINQPad
// https://forum.linqpad.net/discussion/1020/using-namespace-statements-in-c-query-file
// https://spottedmahn.wordpress.com/2010/03/16/linqpad-using-statement-problem/

// Info
// To see the properties of a LINQPad query right-click and select References and Properties.

// ------------------------------------------------------------------------------------------
// Query Optimization ca be achieved by means of two strategies which may also be combined.

// 1-By making use of available multiple cores. 
// PLINQ
// https://docs.microsoft.com/en-us/dotnet/standard/parallel-programming/parallel-linq-plinq

// 2-By recompiling LINQ pipelines into IL imperative code so that the designer retains the
//   adavntage of greater readability without sacrifying performance.
// LinqOptimizer
// https://github.com/nessos/LinqOptimizer
// ------------------------------------------------------------------------------------------


var listSize = 10000000;

// Unoptimized LINQ benchmark
var stopwatch = new Stopwatch();
stopwatch.Restart();
var s = Enumerable.Range(1, listSize)
	.Select(n => n * 2)
	.Select(n => Math.Sin((2 * Math.PI * n)/1000))
	.Select(n => Math.Pow(n,2))
	.Sum();
stopwatch.Stop();
Console.WriteLine("Unoptimized LINQ benchmark {0} in {1}ms", s, stopwatch.ElapsedMilliseconds);

// Simple imperative code
stopwatch.Restart();
double sum = 0;
for (int n = 1; n <= listSize; n++)
{
	var a = n * 2;
	var b = Math.Sin((2 * Math.PI * a)/1000);
	var c = Math.Pow(b,2);
	sum += c;
}
stopwatch.Stop();
Console.WriteLine("Simple imperative code - for loop {0} in {1}ms", sum, stopwatch.ElapsedMilliseconds);





























