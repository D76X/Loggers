<Query Kind="Statements">
  <Reference Relative="Binaries\LinqOptimizer.Base.dll">C:\GitHub\Loggers\Examples\LINQPad\Binaries\LinqOptimizer.Base.dll</Reference>
  <Reference Relative="Binaries\LinqOptimizer.Core.dll">C:\GitHub\Loggers\Examples\LINQPad\Binaries\LinqOptimizer.Core.dll</Reference>
  <Reference Relative="Binaries\LinqOptimizer.CSharp.dll">C:\GitHub\Loggers\Examples\LINQPad\Binaries\LinqOptimizer.CSharp.dll</Reference>
  <Reference Relative="Binaries\MoreLinq.dll">C:\GitHub\Loggers\Examples\LINQPad\Binaries\MoreLinq.dll</Reference>
  <Namespace>MoreLinq.Extensions</Namespace>
  <Namespace>Nessos.LinqOptimizer.Core</Namespace>
  <Namespace>Nessos.LinqOptimizer.CSharp</Namespace>
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

// By Using the LinqOptimizer library available as a NuGet.
// This library makes available the IEnumerable<T>AsQueryExpr() extension method which 
// compiles the LINQ pipeline into imperative code by means of Expressions.
// -------------------------------------------------------------------------------------------------
// As usual in order to use a NuGet pkg in the free version of LinqPad you must firstly download 
// the NuGet using the NuGet Package Explorer and then pull out the binaries from the package which
// is nothing more than a zipped folder into which you can navigate. A reference to these binaries 
// must then be set in LinqPad and lastly, the necessary namespaces must be made available to the 
// LinqPad script.
// C:\GitHub\NuGetPakages
// C:\GitHub\Loggers\Examples\LINQPad\Binaries
// ------------------------------------------------------------------------------------------------
var q = Enumerable.Range(1, listSize)
	.AsQueryExpr()
	.Select(n => n * 2)
	.Select(n => Math.Sin((2 * Math.PI * n) / 1000))
	.Select(n => Math.Pow(n, 2))
	.Sum();
// this is the compilation step from LINQ pipeline to imperative code as an Expression tree.
q.Compile();

stopwatch.Restart();
var s2 = q.Run();
stopwatch.Stop();
Console.WriteLine("LinqOptimizer.CSharp {0} in {1}ms", s2, stopwatch.ElapsedMilliseconds);

// PLINQ
// As PLINQ is built into the .Net Framework it is automatically available as extension on
// IEnumerable<T>.AsParallel.
stopwatch.Restart();
var s3 = Enumerable.Range(1, listSize)
	.AsParallel()
	.Select(n => n * 2)
	.Select(n => Math.Sin((2 * Math.PI * n) / 1000))
	.Select(n => Math.Pow(n, 2))
	.Sum();
stopwatch.Stop();
Console.WriteLine("PLINQ {0} in {1}ms", s3, stopwatch.ElapsedMilliseconds);

// Combined the PLINQ+LinqOptimizer approach.
// Notice that we gain nothing!
var x = Enumerable.Range(1, listSize)
    // slipt on cores
	.AsParallel()
	// to expression tree of the equivalent optimized imperative code
	.AsQueryExpr()
	.Select(n => n * 2)
	.Select(n => Math.Sin((2 * Math.PI * n) / 1000))
	.Select(n => Math.Pow(n, 2))
	.Sum();

stopwatch.Restart();
var s4 = x.Run();
stopwatch.Stop();

Console.WriteLine("PLINQ+LinqOptimizer {0} in {1}ms", s4, stopwatch.ElapsedMilliseconds);









