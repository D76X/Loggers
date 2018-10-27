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

// Given the Bishop in the position C6 of a chessboard which positions on the board can it reach in a single move? 
// Create an enumerable sequence containing the names of each position the Bishop can move to.

// A chessborad can be modelled by a grid.
// Horizontal axis A..H (8 positions)
// Vertical axis 1..8
// A1 is located in the bottom-left corner.

// -------------------------------------
// 8  ¦   ¦   ¦   ¦   ¦   ¦   ¦   ¦   ¦
// -------------------------------------
// 7  ¦   ¦   ¦   ¦   ¦   ¦   ¦   ¦   ¦
// -------------------------------------
// 6  ¦   ¦   ¦ X ¦   ¦   ¦   ¦   ¦   ¦
// -------------------------------------
// 5  ¦   ¦   ¦   ¦   ¦   ¦   ¦   ¦   ¦
// -------------------------------------
// 4  ¦   ¦   ¦   ¦   ¦   ¦   ¦   ¦   ¦
// -------------------------------------
// 3  ¦   ¦   ¦   ¦   ¦   ¦   ¦   ¦   ¦
// -------------------------------------
// 2  ¦	  ¦   ¦   ¦   ¦   ¦   ¦   ¦   ¦
// -------------------------------------
// 1  ¦	  ¦   ¦   ¦   ¦   ¦   ¦   ¦   ¦
// -------------------------------------
// 	  ¦ A ¦ B ¦	C ¦	D ¦	E ¦ F ¦ G ¦ H ¦
// -------------------------------------

int[] horizontal = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
int[] vertical   = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
  
void Main() { 
 
 // tests 
 TestStep0(); // zip
 TestStep1(); // model chessboard with cros product and fluent syntax
 TestStep2(); // model chessboard with cros product and query syntax
  
  // A possible algorithm for the solution.
  
  // From the chessboard filter out those element that cannot be reached 
  // by the Bishop, those element that remains in the resulting enumeration
  // are the positions that can be reached by the Bishop plus the its starting
  // position. This is clearly not the most efficient solution but it's simple
  // to implement with LINQ and the chessboard is so small after all that the 
  // performance warrants lower importance than readibility.
  
  // In the game of chess ranks is the term given to rows of the board while 
  // the term for the columns is files.
  
  var files = Enumerable.Range('a',8);
  var ranks = Enumerable.Range('1',8);
  
  // The position that the Bishop can reach are located on the diagonals 
  // intersecting in the starting postion. If we measure the horizontal 
  // and the vertical distance from the starting position as the numbers
  // of columns (files) and rows (ranks) the constraint for the moves of 
  // the Bishop is that this distances must remain equal for each possible 
  // move. That is if a Bishop's move has a one rank up then it must also 
  // have one file left or right. Again, if the Bishop's move has two ranks
  // down it must also have two files left or right, etc.
  
  var moves1 = files
			  .SelectMany(f => ranks, (f,r) => new {File=(char)f, Rank= (char)r}) // the whole board as IEnumerable of anonymous objects
			  .Where(p => Math.Abs(p.File-'c') == Math.Abs(p.Rank-'6'))			  // the Bishop constraint starting at c6
			  .Where(p => p.File != 'c')										  // remove the initial position which is not a valid Bishop's move
			  .Select(p => $"{p.File}{p.Rank}")									  // just nicer formatting
			  .Dump();
	
    // improved alternative
	var moves2 = files
			  // the whole board as IEnumerable of anonymous objects
			  .SelectMany(f => ranks, (f,r) => new {File=(char)f, Rank= (char)r}) 
			  // augment the anonymous objects that describe each position on the chessboard 
			  // with the information dx & dy which measure the distance of the position from
			  // the c6 cell occupied by the Bishop initially
			  .Select(p => new {
			  					 p.File, 
								 p.Rank, 
								 dx=Math.Abs(p.File-'c'), 
								 dy=Math.Abs(p.Rank-'6')
								 }) 
			  // to satisfy the Bishop's constaint take all the cells for with dx == dy  
			  // and remove the initial position which is not a valid move				 
			  .Where(o => o.dx==o.dy && o.dx!=0)	
			  // provide a nicer formatting for the output
			  .Select(p => $"{p.File}{p.Rank}")									  
			  .Dump();
			  
    // equivalent query syntax
	var moves3 = 
	from v in files
	from h in ranks
	let f = (char)v
	let r = (char)h
	let dx = Math.Abs(f-'c')
	let dy = Math.Abs(r-'6')
	where dx==dy && dx!=0
	select $"{f}{r}";
	// notice that because of defferred execution you must call Dump() on the 
	// IQueryable in order to actually enumerate the expression.
	moves3.Dump();
	
}

void TestStep2(){ 
   
   // a cross product by using the fluent syntax
   // the output models the chessboard
   horizontal
   .SelectMany(h => vertical, (h,v) => new {h,v})
   .Dump();	
}

void TestStep1(){ 
  
  // a cross product by using the query syntax
  // the output models the chessboard  
  (from h in horizontal 
   from v in vertical
   select new {h,v})
   .Dump();	
}

void TestStep0() {
  // this does not perform the cross join it just
  // zip the enumerable into an enumerable of tuples.
  horizontal.Zip(vertical, (h,v) => (h,v)).Dump();
}