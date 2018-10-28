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

// Challenge
// given the data below return the count for each distinct item in it.
string data = "dog,cat,rabbit,dog,dog,lizard,cat,dog,rabbit,pig,dog,cat,pig,lizard,donkey";

void Main() { 
 
 // tests 
 TestStep0();
 	
}

void TestStep0(){
 // IEnumerable<T>.Aggregate is very versatile
 var acc = new Dictionary<string,int>();
 
 var result = data
    .Split(',')	
 	.Aggregate(
 		acc,
 		(dic,next) => { 
			
			if(dic.Keys.Contains(next)){
				dic[next] += 1;
			} else {
				dic[next] = 1;
			}
			
			return dic; 
	});
	
 result.Dump();	
}