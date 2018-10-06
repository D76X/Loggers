<Query Kind="Program">
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


// Given the data in the format below expand it intoa sequence of integer spanning the rage.
string rangeData ="2,5,7-10,11,17-18";

void Main() {

 rangeData.Dump();
 
 // tests 
 // TestStep1();
 // TestStep2();
 // TestStep3();
 // TestStep4();
 
 // process through a pipeline
 // notice the effect of the SelectMany which flattens the IEnumerable<IEnumerable<int>>
 // into IEnumerable<int>.
 rangeData
 	.Split(',')
	.Select(s => { 	
		string[] range = s.Split('-');
		int start = int.Parse(range[0]);
		int rangeSpan = range.Length > 1 ? int.Parse(range[1]) - start + 1: 1;	
		return Enumerable.Range(start, rangeSpan);
     })
	 .SelectMany(s => s)
	 .Dump();
}

void TestStep4(){
rangeData
 	.Split(',')
	.Select(s => { 	
		string[] range = s.Split('-');
		int start = int.Parse(range[0]);
		int rangeSpan = range.Length > 1 ? int.Parse(range[1]) - start + 1: 1 ;	
		return Enumerable.Range(start, rangeSpan);
     })	 
	 .Dump();
}

void TestStep3(){
 rangeData
 	.Split(',')
	.Select(s => { 	
		string[] range = s.Split('-');
		int start = int.Parse(range[0]);
		int rangeSpan = range.Length > 1 ? int.Parse(range[1]) - start + 1: 1 ;	
		return new int[] {start, rangeSpan};
     })
	 .SelectMany(s => s)
	 .Dump();
}

void TestStep2(){
  rangeData
 	.Split(',')
	.Select(s => { 	
		string[] range = s.Split('-');
		int start = int.Parse(range[0]);
		int rangeSpan = range.Length > 1 ? int.Parse(range[1]) - start + 1: 1 ;
		return new int[] {start, rangeSpan};			
     })
	 .Dump();
}

void TestStep1(){
 rangeData
 	.Split(',')
	.Select(s => { 	
		string[] range = s.Split('-');
		return range;
     })
	 .Dump();
}
