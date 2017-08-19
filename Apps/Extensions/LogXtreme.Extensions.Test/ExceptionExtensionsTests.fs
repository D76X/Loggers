module LogXreme.Extensions.Test.ExceptionExtensions

open System
open Xunit
open Swensen.Unquote

// to make extension methods declared in C# visible to F#
// it is necessary to open the namespace of the extensions
// https://stackoverflow.com/questions/777247/using-extension-methods-defined-in-c-sharp-from-f-code
open LogXtreme.Extensions

[<Fact>]
let ``Exception.FullMessage recovers 2 messages``() = 
    
    // arrange
    let om = @"outer"
    let im = @"inner"
    let inner = new Exception(im)
    let outer = new Exception(om,inner)
    let expected = "outer\r\ninner\r\n"
    
    // act
    let actual = outer.FullMessage()    

    // assert
    // Assert.Equal(actual, expected)
    test <@ actual = expected @>
