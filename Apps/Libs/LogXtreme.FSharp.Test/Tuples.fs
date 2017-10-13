module LogXtreme.FSharp.Test.Tuples

// https://fsharpforfunandprofit.com/posts/tuples/

open System
open Xunit
open Swensen.Unquote

let isEven x = (x % 2) = 0
let isOdd x = (x % 2) <> 0

// the following implementation show how to handle "out" parameters
// in .NET in F#.
// https://stackoverflow.com/questions/28691162/the-f-equivalent-of-cs-out
// https://stackoverflow.com/questions/5028377/understanding-byref-ref-and

// It is a common scenario that you want to return two values 
// from a function rather than just one. Tuples are perfect 
// types to use for this recurrent scenarios. This implementation
// handles the exception raised when the parse fails.
let tryParse1 intStr = 
    try 
        let i = System.Int32.Parse intStr
        (true,i)
    with _ -> (false,0)

// This implementation exploits the F# deconstruction of tuples. 
let tryParse2 intStr = 
    let parsed, i = System.Int32.TryParse intStr
    match parsed with
        | true -> (i, true)
        | false -> (0, false)

[<Fact>]
let ``2-tuple breaks into first and second``()=

    // arrange
    let value1 = 1
    let isValue1Even = value1 |> isEven

    let value2 = 2
    let isValue2Even = value2 |> isEven
    
    let test1 = (value1, isValue1Even)
    let test2 = (value2, isValue2Even)

    // act 
    let v1,t1 = test1
    let v2, t2 = test2

    // assert
    test<@ v1 = value1 @>
    test<@ t1 = false @>
    test<@ v2 = value2 @>
    test<@ t2 = true @>

