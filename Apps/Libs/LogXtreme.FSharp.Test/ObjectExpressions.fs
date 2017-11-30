module LogXtreme.FSharp.Test.ObjectExpressions

// https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/object-expressions
// https://fsharpforfunandprofit.com/posts/object-expressions/

// Object Expressions that support multiple interfaces
// https://stackoverflow.com/questions/5789746/can-i-use-the-use-keyword-with-object-expressions
// https://stackoverflow.com/questions/31616761/f-casting-operators

open System
open System.Numerics
open Xunit
open Swensen.Unquote

// object expressions creates a new instances dynamically for anonymous object type 
// based on an existing base type, interface, or set of interfaces.
// With object expression it is possible to implement an interface on-the-fly, without 
// having to create a class.

// Object Expressions that support multiple interfaces
// https://stackoverflow.com/questions/5789746/can-i-use-the-use-keyword-with-object-expressions
// https://stackoverflow.com/questions/31616761/f-casting-operators
// An object expression can have just a single type which is the type of the first interface that
// it implements

[<Fact>]
let ``System.Object with overrridden ToString``()=
    
    // arrange
    let expectedTestValue = @"testValue"
    let objectExpression = {new System.Object() with member x.ToString() = expectedTestValue }

    // act 
    let actual = objectExpression.ToString()

    // assert
    test<@ actual = expectedTestValue @>

[<Fact>]
let ``Object Expression implements IFormattable``()=

    // arrange
    
    // An OE that takes constructor parameters
    let formattableObjExp(delimiter1: string, delimiter2: string) = {
            new System.IFormattable with
                member x.ToString(format: string, provider: System.IFormatProvider) = 
                    if format = @"D" then delimiter1 + x.ToString() + delimiter2
                    else x.ToString()
        }
    
    // use the OE to get an istance
    let del1 = @"{"
    let del2 = @"}"
    let formattingInstruction = @"{0:D}"
    let uut = formattableObjExp(del1, del2)
    let expected = del1+uut.ToString()+del2 

    // act 
    // format the formattable - this ivokes System.Object.ToString() with the formatting
    // instructions
    let actual = System.String.Format(formattingInstruction, uut)

    // assert
    test<@ actual = expected @>

type IAdder = 
    abstract member Add: int -> int -> int

[<Fact>]
let ``Object Expression implements custom IAdder``()=

    // arrange
    let AdderObjExp(delta: int) = {
        new IAdder with
            member this.Add f s = f + s + delta }
    
    let d = 10
    let uut = AdderObjExp(d)
    let first = 1
    let second = -5
    let expected = first + second + d

    // act 
    let actual = uut.Add first second

    // assert
    test <@ actual = expected @>

type IMultiply = 
    abstract member Multiply: int -> int -> int

// https://stackoverflow.com/questions/5789746/can-i-use-the-use-keyword-with-object-expressions
[<Fact>]
let ``Object Expression implements custom IAdder and IMulty``()=

    // arrange 
    // OE that implements multiple interfaces 
    let AddAndMulObjExp(delta: int, factor: int) = {    

        // on OE cannot use this interface pattern
        //member this.Multiply (f: int) (s: int) =
        //    (this :> IMultiply).Multiply f s

        new IAdder with
            member this.Add f s = f + s + delta 
        interface IMultiply with
                member this.Multiply f s = f * s * factor}
    
    let delta = 10    
    let factor = 2
    let first = 1
    let second = -5
    let uut = AddAndMulObjExp(delta, factor)
    let expectedSum = first + second + delta
    let expectedProduct = first * second * factor

    // act 
    let actualSum = uut.Add first second
    // only the first iterface is directly visible
    // the other implemented interfaces require a downcast
    let actualProduct = (uut :?> IMultiply).Multiply first second

    // assert
    test<@ actualSum = expectedSum @>
    test<@ actualProduct = expectedProduct @>

[<AbstractClass>]
type Metric() = 
    abstract member Distance : float -> float -> float

[<Fact>]
let ``Object Expression implements abstract class Metric``() =

    // arrange 
    let PwrMetric(exp: float, shift: float) = {        
            new Metric() with
                member this.Distance f s = (f-shift)**exp+(s-shift)**exp
        } 
    
    let exp = 2.
    let shift = 1.    
    let uut = PwrMetric(exp, shift)
    let first = 3 
    let second = 5    
    let expectedDistance = 20.

    // act 
    let actualDistance = uut.Distance (float first) (float second)

    // assert
    test<@ actualDistance = expectedDistance @>







