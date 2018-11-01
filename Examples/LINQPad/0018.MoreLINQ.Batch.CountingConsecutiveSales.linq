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
// given a record of the number of sales carried out each day by a saleperson find the longest
// streak of consecutive days in which somethong was sold.
// In this specific case the result should be 2, 1, 5, 4.
string data = "0, 1, 3, 0, 0, 2, 1, 5, 4, 0, 0, 0, 3";

void Main() { 
 
 // tests 
 //TestStep0();
 //TestStep1();
 //TestStep2();
 TestStep3();
 
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
  
 // Here the MoreLINQ Batch method makes the expression of the logic very elegant
 var result = 
	 data	
	.Split(',')
	.Select(s => s.Trim());
	
 result.Dump();
 
}

void TestStep0() {

	// this way is rather imperative
	// we show the intermediate step in which we build an anonymous type that may be used for a groub by 
	// strategy.
	// https://stackoverflow.com/questions/4049773/how-to-use-index-position-with-where-in-linq-query-language
	int @block = 0;
	var result = 
	from d in data.Split(',').Select((s,index) => {
		int sales = Int32.Parse(s.Trim());
		@block = sales==0 ? ++@block : @block; 
		return new {
		Block=$"{@block}", 
		Day=++index, 
		Sales = sales};		
		})	
	select d;
	
	result.Dump();
}

void TestStep1() {

	// Following from the previous step.
	int @block = 0;
	var result = 
	from d in data.Split(',').Select((s,index) => {
		int sales = Int32.Parse(s.Trim());
		@block = sales==0 ? ++@block : @block; 
		return new {
		Block=$"{@block}", 
		Day=++index, 
		Sales = sales};		
		})
	group d by new { d.Block };
	
	result.Dump();
}

void TestStep2() {

	// Following from the previous step.
	int @block = 0;
	var result = 
	from d in data.Split(',').Select((s,index) => {
		int sales = Int32.Parse(s.Trim());
		@block = sales==0 ? ++@block : @block; 
		return new {
		Block=$"{@block}", 
		Day=++index, 
		Sales = sales};		
		})
	group d by new { d.Block }
	// The Into keyword allows us to create a temporary variable to store the results of a group, join, or select clause into a variable.
	into gSales 
	// Take only the IGrouping that are not single zero sales.
	where gSales.Count()>1 
	select new { gSales };

	
	result.Dump();
}

void TestStep3() {

	// Following from the previous step.
	int @block = 0;
	var result = 
	from d in data.Split(',').Select((s,index) => {
		int sales = Int32.Parse(s.Trim());
		@block = sales==0 ? ++@block : @block; 
		return new {
		Block=$"{@block}", 
		Day=++index, 
		Sales = sales};		
		})
	group d by new { d.Block }
	// The Into keyword allows us to create a temporary variable to store the results of a group, join, or select clause into a variable.
	into gSales 
	// Take only the IGrouping that are not single zero sales.
	let groupCount = gSales.Count()	
	where groupCount>1
	// order the remaining group from the one with more consecutive days in descending order
	orderby gSales.Count() descending
	select new { gSales };

	// take only the first group which holds the longest streak of consecutive sales.
	result.Take(1).Dump();	
}








