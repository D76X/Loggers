module LogXtreme.FSharp.Test.Functions

// Functions are First-Class Values in F#
// https://docs.microsoft.com/en-us/dotnet/fsharp/introduction-to-functional-programming/functions-as-first-class-values

open System
open System.Numerics
open Xunit
open Swensen.Unquote
//---------------------------------------------------------------------------------------------
// Typical measures of first-class status include the following:

// 1- Can you bind functions to identifiers? That is, can you give them names?
// 2- Can you store functions in data structures, such as in a list?
// 3- Can you pass a function as an argument in a function call?
// 4- Can you return a function from a function call?

//---------------------------------------------------------------------------------------------

// Some important things to know about F# functions

// Equality with values of type function.
// https://stackoverflow.com/questions/8225433/checking-function-equality-in-a-f-unit-test

// The F# compiler generates a class that inherits from FSharpFunc when you return a function 
// as a value. More importantly, it generates a new class each time you create a function value, 
// so you cannot compare the types of the classes.

//---------------------------------------------------------------------------------------------

//[<Fact>]
//let ``test First-Class function can be named``() =
    
    // arrange
    //let squareIt = fun n -> n * n   

    // act 
    //let otherNameOf_squareIt = squareIt 

    // assert

    // Functions do not define equality and the follwong is impossible!
    // https://stackoverflow.com/questions/8225433/checking-function-equality-in-a-f-unit-test
    //-----------------------------------------
    //test<@ otherNameOf_squareIt = squareIt @>   
    //-----------------------------------------