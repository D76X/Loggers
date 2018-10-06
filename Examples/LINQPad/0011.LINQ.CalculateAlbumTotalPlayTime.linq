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
// To see the properties of a LINQPad quesry right-click and select References and Properties.


// Given the playtime of the individual traks in an Album compute the total playtime. 

// minutes:seconds
string playtimeData ="2:54,3:48,4:51,3:32,6:15,4:08,5:17,3:13,4:16,3:55,4:53,5:35,4:24";

void Main() {

 // run this test to figure out how to compose the fomrat string for time intervals.
 // TestTimeSpanFormatString();
 
 // show the intermediate step
 // TurnTheStringIntoAnEnumerableOfTimeStamps();

 // test IEnumerable Sum extension method
 // TestSumOfEnumerable();
 
 // the whole functional implementation
 // Summing TimeSpan data by using a projection and intergal value
 // https://stackoverflow.com/questions/4703046/sum-of-timespans-in-c-sharp
 var playTimeTicks = playtimeData
 			     	.Split(',')							  
				 	.Select(s => TimeSpan.ParseExact(s,@"m\:ss", CultureInfo.InvariantCulture))
				 	.Sum(ts => ts.Ticks);

 var playTime = new TimeSpan(playTimeTicks);
							  
 playTime.Dump();
 
 // Alternatively the Aggregate extension method may be used.
 // In this way it is possible to use the + operator as defined on the TimeSpan type.
 var totalSpan = playtimeData
 			     .Split(',')							  
				 .Select(s => TimeSpan.ParseExact(s,@"m\:ss", CultureInfo.InvariantCulture))
				 .Aggregate(TimeSpan.Zero, (accumulator, next) => accumulator += next); 
				 
 totalSpan.Dump();
 
 // even shorter
 // notice that the accumulator is automatically set to the default value for the TimeSpan type
 // which of course is TimeSpan.Zero
 playtimeData
	.Split(',')							  
	.Aggregate((accumulator, next) => accumulator += TimeSpan.ParseExact(s,@"m\:ss", CultureInfo.InvariantCulture))
	.Dump(); 
}

void TestSumOfEnumerable(){
 IEnumerable<int> data = new int[] {1,2,3}; 
 data.Sum().Dump();
}

void TurnTheStringIntoAnEnumerableOfTimeStamps(){

 // turn the string into an array of TimeSpan by splitting it.
 var playtimes = playtimeData
 			     .Split(',')							  
				 .Select(s => TimeSpan.ParseExact(s,@"m\:ss", CultureInfo.InvariantCulture));			 				 
							  
 playtimes.Dump();

}

void TestTimeSpanFormatString() {

 // Refs - Custom TimeSpan Format String
 // https://docs.microsoft.com/en-us/dotnet/standard/base-types/custom-timespan-format-strings
 TimeSpan interval;
 string value = "16:32.05";
 TimeSpan.TryParseExact(value, @"mm\:ss\.ff", null, out interval);
 interval.Dump();
 
 value = "2:54";
 TimeSpan.TryParseExact(value, @"m\:ss", null, out interval);
 interval.Dump();
 interval = TimeSpan.ParseExact(value, @"m\:ss", null);
 interval.Dump(); 
 
 value = "12:54";
 TimeSpan.TryParseExact(value, @"m\:ss", null, out interval);
 interval.Dump();
 interval = TimeSpan.ParseExact(value, @"m\:ss", null);
 interval.Dump();  
}



