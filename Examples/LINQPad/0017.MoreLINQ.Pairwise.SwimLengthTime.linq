<Query Kind="Program">
  <Reference Relative="Binaries\MoreLinq.dll">C:\GitHub\Loggers\Examples\LINQPad\Binaries\MoreLinq.dll</Reference>
  <Namespace>static MoreLinq.Extensions.PairwiseExtension</Namespace>
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

// Challenge
// given the data below representing the time taken when a swimmer completes 
// a pool's length convert the sequence into a new sequnce of the time taken 
// by the swimmer to swim each length. In other words compute the sequence of
// differences.
// Example of the expected output
// Length 1: Start = 0:00 End = 0:45  Delta = 0:45
// Length 2: Start = 0:45 End = 01:32 Delta = ...
// etc.

string data = "00:45,01:32,02:18,03:01,03:44,04:31,05:19,06:01,06:47,07:35";

void Main() { 
 
 // tests 
 TestStep0();
 TestStep1();
 
 //----------------------------------------------------------------------------------------------------
 // How to reference MoreLINQ in the free version of LINQPad!
 // 1- Use NuGet Package Explorer to download the NuGet Package from the remote repo to a local folder
 // 2- C:\GitHub\NuGetPakages
 // 3- Use NuGet Package Explorer to inspect the content of the NuGet pckg file
 // 4- Save the DLL to a folder where LINQPad can have access
 // 5- C:\GitHub\Loggers\Examples\LINQPad\Binaries
 // 6- Add a refernce to this binary in LINQPad i.e. F4...
 // 7- add any relevant namespace i.e. MoreLinq
 //----------------------------------------------------------------------------------------------------
 // MoreLINQ namespaces 
 //
 // https://markheath.net/post/exploring-morelinq-16-pairwise
 // https://stackoverflow.com/questions/31852389/how-do-i-use-the-c6-using-static-feature
 //----------------------------------------------------------------------------------------------------
  
 // we can obtain a more elegant and efficinet solution by means of 
 // the MoreLINQ library Pairwise extension method. Pairwise takes 
 // a sequence and turn it into a sequence of adjecent elements.
 // It is actually a special case of the MoreLINQ Windows extension
 // method which allows 
 var result = 
	 data	
	.Split(',')	
	.Select(s => TimeSpan.Parse($"00:{s}"))	
	// Prepend is implemented in System.LINQ and MoreLINQ
	// here we use the LINQ standard implementation as we 
	// have imported Pairwise by means of thestatic namespace
	// static MoreLinq.Extensions.PairwiseExtension
	.Prepend(TimeSpan.Zero)					
	.Pairwise((a,b)=> b-a);
	
 result.Dump();
 
}

class Rec {
	string Start {get; set;}
	string End{get; set;}
	string Delta{get; set;}
}

void TestStep0(){ 
	
	// one ugly way combines 
	// 1- IEnumerable<T>.Aggregate
	// 2- anonymous types
	// 3- the use of the dynamic keyword
	// 4- use of the ArrayList type
	var result = 
	data
	.Split(',')
	.Aggregate(
		new ArrayList(),
		(agg,next) => {			
			int index = agg.Count-1;
			string start = "00:00";			
			if(index<0) {
				agg.Add(new {Start=start, End=next, Delta=TimeSpan.Parse(next)-TimeSpan.Parse(start)});
				return agg;
			}				
			dynamic last = agg[index];	
			start = last.End;
			agg.Add(new {Start=last.End, End=next, Delta=TimeSpan.Parse(next)-TimeSpan.Parse(start)});
			return agg;
		});
		
	result.Dump();
}

void TestStep1(){ 
	
	// this is even worse than before because it splits 
	// the data twice! This means that its time complexity
	// is 2N instead of N. 
	var result = 
	("00:00," + data)
	.Split(',')
	.Zip(
		data.Split(','),
		(s,f) => { 
		 var start = TimeSpan.Parse(s);
		 var finish = TimeSpan.Parse(f);
		 return new {
		 Start = start,
		 Finish = finish,
		 Delta = finish - start 
		 };
	});
		
	result.Dump();
}