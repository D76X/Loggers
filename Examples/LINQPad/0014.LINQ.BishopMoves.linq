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


// Given the Bishop in the position C6 of a chessboard which positions on the can it reach in a sinngle move? 
// Create an enumerable sequence containing the names of each position the Bishop can move to.

// A chessborad can be modelled by a grid.
// Horizontal axis A..H (8 positions)
// Vertical axis 1..8
// A1 is located in the bottom-left corner.

void Main() {

 nameDobData.Dump();
 
 // tests 
 // TestStep1();
 // TestStep2();
 // TestStep3();
 // TestStep4();
 // TestStep5();
 
  nameDobData
	.Split(';')
	.Where(str => !String.IsNullOrEmpty(str))    
	.Select(t => t.Split(','))
	.Where(a => a.Length > 0)
	.Select(a => new {	
						name = a[0].Trim(), 
						age = a.Length>1 ? 
							ParseDob(a[1]).DobToAge() : 
							DateTime.MinValue.DobToAge()
						})
	.OrderBy(o => o.age)
	.ThenBy(o => o.name)
	.Dump();
 
}

void TestStep1(){ 	
	
}