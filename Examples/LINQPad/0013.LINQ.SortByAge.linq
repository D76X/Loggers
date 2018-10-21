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


// Given the data in the format below output the list of names and age sorted by ascending age.
string nameDobData ="Jason Puncheon, 26/06/1986;Jos Hooiveld, 22/04/1983;Kelvin Davis, 29/09/1976; Luke Shaw, 12/07/1995; Gaston Ramirez, 02/10/1976;";

// Calculate the difference between two dates and get the value in years?
// https://stackoverflow.com/questions/3152977/calculate-the-difference-between-two-dates-and-get-the-value-in-yearss
static class DateTimeExtensions {

	public static int DobToAge(this DateTime dob) { 
    	var now = DateTime.Now;
		int age = now.Year - dob.Year;
		if (dob > now.AddYears(-age)) {age--;};
		return age;
		}
}

static DateTime ParseDob(string dob) => DateTime.ParseExact(dob.Trim(), @"dd/MM/yyyy", CultureInfo.InvariantCulture);

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

void TestStep5(){ 
	
	// 1-notice that @"dd/MM/yyyy" does not produce the same output as @"dd/mm/yyyy".
	// 2-the @"d/M/yyyy" may also be used instead of @"dd/MM/yyyy" if single digit day and month are expected.
	// 3-CultureInfo.InvariantCulture may be replaced by new CultureInfo("en-GB") for example if the data source is from a known culture.
	
	nameDobData
	.Split(';')
	.Where(str => !String.IsNullOrEmpty(str))    
	.Select(t => t.Split(','))
	.Where(a => a.Length > 0)
	.Select(a => new {	
						name = a[0].Trim(), 
						age = a.Length>1 ? 
							DateTime.ParseExact(a[1].Trim(), @"dd/MM/yyyy", CultureInfo.InvariantCulture).DobToAge() : 
							DateTime.MinValue.DobToAge()
						})		
	.Dump();
}

void TestStep4(){ 
	
	// 1-notice that @"dd/MM/yyyy" does not produce the same output as @"dd/mm/yyyy".
	// 2-the @"d/M/yyyy" may also be used instead of @"dd/MM/yyyy" if single digit day and month are expected.
	// 3-CultureInfo.InvariantCulture may be replaced by new CultureInfo("en-GB") for example if the data source is from a known culture.
	
	nameDobData
	.Split(';')
	.Where(str => !String.IsNullOrEmpty(str))    
	.Select(t => t.Split(','))
	.Where(a => a.Length > 0)
	.Select(a => new {	
						name = a[0].Trim(), 
						dob = a.Length>1 ? DateTime.ParseExact(a[1].Trim(), @"dd/MM/yyyy", CultureInfo.InvariantCulture) : DateTime.MinValue 
						})
	.Dump();
}

void TestStep3(){ 	
	nameDobData
 	.Split(';')
    .Where(str => !String.IsNullOrEmpty(str))    
	.Select(t => t.Split(','))
	.Where(a => a.Length > 0)
	.Select(a => new { name = a[0].Trim(), dob = a.Length>1 ? a[1].Trim() : "0" })
	.Dump();
}

void TestStep2(){ 	
	nameDobData
	.Split(';')
    .Select(t => t.Split(','))
	.Dump();
}

void TestStep1(){ 	
	nameDobData
	.Split(';')
	.Dump();
}