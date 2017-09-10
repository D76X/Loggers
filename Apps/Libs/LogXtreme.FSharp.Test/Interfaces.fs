module LogXtreme.FSharp.Test.Interfaces

// https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/interfaces
// https://fsharpforfunandprofit.com/posts/interfaces/
// https://stackoverflow.com/questions/11812250/overriding-an-inherited-member-in-f
// https://en.wikibooks.org/wiki/F_Sharp_Programming/Interfaces
// https://stackoverflow.com/questions/31825587/f-interfaces-and-properties
// https://stackoverflow.com/questions/14593771/f-and-interface-implemented-members

open System
open System.Numerics
open Xunit
open Swensen.Unquote

// F# interface declaration example
// all members are declared by signature only
type ICombinator = 
    abstract member Combine: float -> float -> float
    abstract member Factor: float
    abstract member Weight: int with get, set

// F# abstract classes are declared in a very similar fashion to interface
// However the [<AbstractClass>] and tells the F# compiler the difference
// Interfaces have no constructors while abstract classes define a constructor
// by following the name of the type with the ()
[<AbstractClass>]
type CombinatorBase() = 
    abstract member Combine: float -> float -> float
    abstract member Factor: float
    abstract member Weight: int with get, set

// In F#, all interfaces must be explicitly implemented
// The "this" accessor is used to declare the implementation of the interface on the
// supporting type. This implementation is naive because the constructor may pass a
// weight o zero that would cause a divide-by-zero exception.
type PixCombinatorNaive(w: int) = 

    // https://stackoverflow.com/questions/14593771/f-and-interface-implemented-members
    // by defining a member on the supporting type with the same name as the interface
    // member that is implemented the caller will no longer need the explicit upcast
    // which is done by the implementing type. It also possible to combine members of 
    // the iterface.
    member this.Combine (f: float) (s: float) = 
        ((this :> ICombinator).Combine f s)*(this :> ICombinator).Factor / (float)(this :> ICombinator).Weight 

    // explicit implementation of the interface ICombinator
    // cannot use a member of ICombinator in another member of the same interface
    interface ICombinator with         

        // use "this"
        member this.Combine first second =
            first * second
        
        // use "this"    
        member this.Factor: float = Math.PI   
        
        // here "this" is not used (not clear why)
        // https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/members/properties
        member val Weight: int = w with get, set 
        

[<Fact>]
let ``PixCombinatorNaive implements ICombinator``() = 

    // arrange
    let weight = 2
    let pixCombinator = PixCombinatorNaive(weight) 
    let first = 2.1
    let second = 3.7        
    let expected = first * second * Math.PI / (float)weight

    // act
    let combined = pixCombinator.Combine first second 

    // assert
    Assert.Equal(expected, combined)

[<Fact>]
let ``PixCombinatorNaive throws divide-by-zero exception with weight = 0``() =
    
    // arrange
    let weight = 0
    let pixCombinator = PixCombinatorNaive(weight) 
    let first = 2.1
    let second = 3.7        
    let expected = first * second * Math.PI / (float)weight
    
    // See also Casting.fs
    // https://stackoverflow.com/questions/31616761/f-casting-operators
    let invokation = fun() -> ((pixCombinator.Combine first second) :> obj)
    //let value = invokation() 
    
    // Act

    // With xUnit Record.Exception 
    // https://stackoverflow.com/questions/11013025/how-to-test-for-exceptions-thrown-using-xunit-subspec-and-fakeiteasy
    let ex = Record.Exception(fun() -> ((pixCombinator.Combine first second) :> obj))

    // act & assert
    Assert.IsType(typeof<DivideByZeroException>, ex)
    // this is also valid
    //let ex2 = Assert.Throws<DivideByZeroException>(invokation)
    //test<@ typeof<DivideByZeroException> = ex2.GetType() @>

// This cobinator avoids the issue of divide-by-zero exception 
// https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/members/constructors
type PixCombinator(w: int) = 
    
    let mutable weight = if(w=0) then 1 else w 
    
    interface ICombinator with         

        member this.Combine first second =
            first * second
            
        member this.Factor: float = Math.PI           

        member val Weight: int = weight with get, set 


   
//type PipCombinator(w: float) = 


// The interface/end keywords can be used to delimit the abstract members on the 
// interface declaration
type IScale = 
    interface
        abstract ScaleUp: float -> float
        abstract ScaleDown: float -> float
    end



