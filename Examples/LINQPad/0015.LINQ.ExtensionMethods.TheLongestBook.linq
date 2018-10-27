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
// give an enumerable of Books find the Title if the longest book.

public static class LinqExtensions {

	public static TSource MaxBy<TSource, TKey>(
		this IEnumerable<TSource> source,
		Func<TSource, TKey> selector) {
		
		var comparer = Comparer<TKey>.Default;
		
		using(){
		
		}	
	}
}

public class Book {
 public Book(string author, string title, int pages){
 	Author=author; Title=title; Pages=pages;
 }
 public string Author {get; set;}
 public string Title {get; set;}
 public int Pages {get; set;}
}

Book[] books = new Book[] {
	new Book("Author1","Title1", 343),
	new Book("Author2","Title2", 473),
	new Book("Author3","Title3", 122),
	new Book("Author4","Title4", 297)
};

void Main() { 
 
 // tests 
 TestStep0();
 TestStep1();
 TestStep2();
 TestStep3();
 
 // a better way is to define an extension method MaxBy 
 // which can return complex types contrary to the LINQ
 // IEnumerable.Max method.
 
	
}

void TestStep0(){
 // a very BAD naive implementation!
 // it has time complexity N^2!
 
 books.FirstOrDefault(b => b.Pages==books.Max(x => x.Pages)).Dump();
}

void TestStep1(){
 // a very simple, naive implementation
 var max = books
 .Select(b => b.Pages)
 .Max();
 
 books.Where(b => b.Pages==max).Dump();
}

void TestStep2(){
 
 // a very simple, naive implementation
 // requires sorting thus it is of the same 
 // complexity as finding the max first
 books.OrderBy(b => b.Pages).Last().Dump();
}

void TestStep3(){
 
 // here we use the versitile IEnumerable<T>.Agreegate function
 // although it does not warrant good readability. The aggregate
 // value 'max' is a complex value and the time complexity is N
 // as in the cases before.
 books.Aggregate((max,next)=> next.Pages > max.Pages ? next : max).Dump();
}
