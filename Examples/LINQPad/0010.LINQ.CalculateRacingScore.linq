<Query Kind="Program" />

// C# Program
// ITs like you are in the body of the classic static class Program of a console app

// Refs 
// https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable?view=netframework-4.7.2

// Expression 0
// Given the scores of a set of races as a string work out the score for the racer
// as the sum of the score from each race excluded the three lowest individual scores. 
// "10,5,0,8,10,1,4,0,10,1"

// Expression 1
string scoresData ="10,5,0,8,10,1,4,0,10,1";

void Main() {	

 // turn the string into an array of intergers by splitting it
 // and casting to integer type from string type
 IEnumerable<int> scores = scoresData
 							  .Split(',')
							  .Select(s => { 
							  	int i;
							  	Int32.TryParse(s, out i);
								return i;
							  });
 
 
 // imperative implementation
 var scoreImperative = ComputeScoreImperative(scores);
 scoreImperative.Dump();
 
 // functional implementation
 var scoreFunctional = ComputeScoreFunctional(scores);
 scoreFunctional.Dump();
}

// we assume that there are always more than  3 scores.
double ComputeScoreImperative(IEnumerable<int> scores){ 

	double result = 0.0;
	
	// cannot do this on IEnumerable!
	//scores.Sort();
	
	// in C# parameters are passed to method by value so 
	// the scores reference is a 4-byte (Int32) that holds
	// initialy the same address fo the reference passed
	// to it by the caller. The following assign statement
	// changes the address to that of a new list in memory.
	scores = scores.ToArray();
	int[] _scores = scores as int[];
	
	// sort the array in place ascending
	Array.Sort(_scores);
	
	// skip the first three scores
	for(int i = _scores.Length-1; i > 2 ; i--){
		result+=_scores[i];
	}
	
	return result;
}


double ComputeScoreFunctional(IEnumerable<int> scores){
	
	// one liner LINQ pipeline	
	return scores
		   .OrderBy(s => s)
		   .Skip(3)
		   .Sum();
}

// this is a wrong implementation because it finds the three
// lowest scores without repetions
double ComputeScoreImperativeWrong(IEnumerable<int> scores){ 

 	 double result = 0.0; 
 	 
	 int smallest1 = 0;
	 int smallest2 = 0;
	 int smallest3 = scores.First();
	 
	 foreach(int score in scores){
		
		// find the three lowest individual scores
		if(score < smallest1){
		   smallest1 = score;
		} else if(score < smallest2){
		 smallest1 = smallest2;
		 smallest2 = score;
		} else if(score < smallest3) {
		    smallest1=smallest2;
			smallest2=smallest3;
			smallest3=score;
		}
		
		// Use these to check the algo
		//smallest1.Dump();
	 	//smallest2.Dump();
	 	//smallest3.Dump();
		
		result += score;
	 }	 
	 
	 result += -(smallest1+smallest2+smallest3);
	 
	 return result;
}











