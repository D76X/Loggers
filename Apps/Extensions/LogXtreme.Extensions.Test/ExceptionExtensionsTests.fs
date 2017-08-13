module LogXreme.Extensions.Test.ExceptionExtensions

open System
open Xunit
open Swensen.Unquote

// to make extension methods declared in C# visible to F#
// it is necessary to open the namespace pf the extension
// https://stackoverflow.com/questions/777247/using-extension-methods-defined-in-c-sharp-from-f-code
open LogXtreme.Extensions

[<Fact>]
let ``1 plus 2 gives 3``() = 
    Assert.Equal(3,1+2) 

[<Fact>]
let ``2 plus 2 gives 4``() = 
    test <@ (2+2) = 4 @>

[<Fact>]
let ``Exception.FullMessage recovers 2 messages``() = 
    
    // arrange
    let om = "outer"
    let im = "inner"
    let inner = new Exception(im)
    let outer = new Exception(om,inner)
    let expected = "outer\ninner"
    
    // act
    let actual = outer.FullMessage()    

    // assert
    // Assert.Equal(actual, expected)
    test <@ actual = expected @>
