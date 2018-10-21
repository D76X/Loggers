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
  
  // this does not perform the cross join!
  // var chessBoard = horizontal.Zip(vertical, (h,v) => (h,v));
 
 // tests 
 // TestStep1();
 // TestStep2();
 // TestStep3();
 // TestStep4();
 // TestStep5();
 
 // model the chessboard with an array hxv
  var chessBoard1 = horizontal
   .SelectMany(h => vertical, (h,v) => new {h=h,v=v})
   .ToArray();
   //.Dump();   
  
  // we must select only the elements of the the chessboard 
  // that can be reached by the Bishop from a given position.
  // The C6 posistion maps to the [2,5] index in the array that 
  // models the board.
  
  // The following is not the most efficient algorithm but it's simple
  // and the chessboard is small after all.
  // From the chessboard filter out those element that cannot be reached 
  // by the Bishop.
  
  // Given a starting position the Bishop can only be placed in those
  // point of the grid.
  
  // columns
  var files = Enumerable.Range('a',8);
  // rows
  var ranks = Enumerable.Range('1',8);
  
  // The position that the Bishop can reach are located on diagonals 
  // intersecting in the starting postion. If we measure the horizontal 
  // and the vertical distance from the starting position as the numbers
  // of columns (files) and rows (ranks) the constraint for the Bishop is
  // that this distances must remain equal that is if the Bishop moves one
  // rank up then it must also move one file left or right. If the Bishop
  // moves one rank down it must also move ove file left or right.
  
  var moves1 = files
			  .SelectMany(f => ranks, (f,r) => new {File=(char)f, Rank= (char)r}) // the whole board
			  .Where(p => Math.Abs(p.File-'c') == Math.Abs(p.Rank-'6'))			  // the Bishop constraint
			  .Where(p => p.File != 'c')										  // remove the initial position which is not a valid move
			  .Select(p => $"{p.File}{p.Rank}")									  // just nicer formatting
			  .Dump();
	
    // alternative
	var moves2 = files
			  .SelectMany(f => ranks, (f,r) => new {File=(char)f, Rank= (char)r}) // the whole board
			  .Select(p => new {p.File, p.Rank, dx=Math.Abs(p.File-'c'), dy=Math.Abs(p.Rank-'6')}) // all in one anonymous object			  
			  .Where(o => o.dx==o.dy && o.dx!=0)										  // remove the initial position which is not a valid move
			  .Select(p => $"{p.File}{p.Rank}")									  // just nicer formatting
			  .Dump();
}

void TestStep2(){ 
  // a cross product by using the fluent syntax
   horizontal
   .SelectMany(h => vertical, (h,v) => new {h,v})
   .Dump();	
}

void TestStep1(){ 
  // one way to obtain a cross product by using the query syntax
  (from h in horizontal 
   from v in vertical
   select new {h,v})
   .Dump();	
}