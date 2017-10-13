module LogXtreme.FSharp.Test.Tuples

// https://fsharpforfunandprofit.com/posts/tuples/

open System
open System.Numerics
open Xunit
open Swensen.Unquote

let isEven x = (x % 2) = 0
let isOdd x = (x % 2) <> 0

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

