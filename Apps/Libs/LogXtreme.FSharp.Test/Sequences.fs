module LogXtreme.FSharp.Test.Sequences

// https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/records
// https://fsharpforfunandprofit.com/posts/elevated-world-4/

open System
open System.Numerics
open Xunit
open Swensen.Unquote

//----------------------------------------------------------------------------------------

//Sequences

//A sequence is a logical series of elements all of one type. Sequences are particularly 
//useful when you have a large, ordered collection of data  but do not necessarily expect 
//to use all of the elements.  Individual sequence elements are computed only as required.
//a sequence can provide better performance than a list in situations in which not all the 
//elements are used.  Sequences are represented by the seq<'T> type, which is an alias for 
//IEnumerable<T>. This is an important point that is seq<'T> = IEnumerable<T> and not the 
//non generic interface IEnumerable. 

//Why is this difference important?

//https://stackoverflow.com/questions/5726566/seq-iter-vs-for-what-difference  

//----------------------------------------------------------------------------------------

