module LogXtreme.FSharp.Test.Printfn

// https://fsharpforfunandprofit.com/posts/printf/

open System
open System.Numerics
open Xunit
open Swensen.Unquote


//-------------------------------------------------------------------------------------
// This is the C-style format string:
// printf preferred and considered idiomatic for F#
//-------------------------------------------------------------------------------------
// -1 It is statically type checked - both for the types of the parameters and number.
// -2 It is a well-behaved F# function and so supports partial application, etc.
// -3 It supports native F# types.
// -4 It support partial application.
//-------------------------------------------------------------------------------------

//printfn "A string: %s. An int: %i. A float: %f. A bool: %b" "hello" 42 3.14 true

//-------------------------------------------------------------------------------------

// These would not compile as there is design time checking.

// wrong parameter type as 42 in int and %s means the text formatter expects a string
// printfn "A string: %s" 42 

// wrong number of parameters
// printfn "A string: %s" "Hello" 42

//-------------------------------------------------------------------------------------
// This is the typical .NET style
// This style is not statically typed cheked thus it can cause run time errors.
//-------------------------------------------------------------------------------------

// Typical usage
System.Console.WriteLine("A string: {0}. An int: {1}. A float: {2}. A bool: {3}","hello",42,3.14,true)

// wrong parameter type  but it works nonetheless
System.Console.WriteLine("A string: {0}", 42)   

// wrong number of parameters
System.Console.WriteLine("A string: {0}","Hello",42)  //works!
System.Console.WriteLine("A string: {0}. An int: {1}","Hello") //FormatException
//----------------------------------------------------------------------------------------------

// printf supports partial application

// partial application with explicit parameters
let printStringAndInt s i =  printfn "A string: %s. An int: %i" s i 
let printHelloAndInt i = printStringAndInt "Hello" i
do printHelloAndInt 42

// partial application - point free style

//------------------------------------------------------------
//F# printf string and TextWriterFormat<`T> 
//https://stackoverflow.com/questions/9440204/f-printf-string
//------------------------------------------------------------

//Examples

// does not work 
// printfn expects a TextWriterFormat<`T> not a string
// and cannot convert a string to TextWriterFormat<`T>
let test = "aString"
//let callMe = printfn test

// it works
// ("%s" test) is of type TextWriterFormat<`T>
//printfn "%s" test

//==============================================================================================
