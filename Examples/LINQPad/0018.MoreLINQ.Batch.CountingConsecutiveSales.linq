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
	.Split(',');
	
 result.Dump();
 
}