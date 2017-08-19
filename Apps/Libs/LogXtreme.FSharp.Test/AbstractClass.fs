module LogXtreme.FSharp.Test.AstractClass

// https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/casting-and-conversions
// https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/symbol-and-operator-reference/
// https://fsharpforfunandprofit.com/posts/inheritance/

open System
open System.Numerics
open Xunit
open Swensen.Unquote

type BaseClassWithAParameter(param1) = 
    member this.Prop1 = param1

type DerivedClassWithTwoParameters(param1, param2) =
    inherit BaseClassWithAParameter(param1)
    member this.Prop2 = param2

//-----------------------------------------------------------------------

// One of the major goals of class inheritance is to declare abstract 
// types.

// the abstract member is declared by means of its type annotation (:)
// the abstract member remains unbound in the base class as there is 
// not a (=) to bind the member F to an expression declared to the right
// of the (=)

//-----------------------------------------------------------------------

// in F# when a class declares an abstract member it is required that 
// at least a derived type exists which must implement the abstract 
// member.If such implementing class is not provided the compiler 
// complains with the error :

// "No implementation was given for the abstract member..."

// This error can be removed in three ways 

// 1- By providing a derived class that implements the abstract member.

// 2- By decoration the class declaring the abstract member with the 
//    attribute [<AbstractClass>].

// 3- By providing a default implementation of the declaring base type.
//    See later.

// The attribute [<AbstractClass>] is used to tell the compiler that the designer instends to 
// designer declares an abstract class but not necessarily intends to 
// provide any implementation yet. Normally, declaring a type with an 
// abstract member without providing at least an implementation is an 
// indication of a possible misdesign, this is why the F# CT checking 
// behaves the way it does.


// This would error at CT
// ------------------------------------------
//type BaseClassX() = 
//    abstract member Op : int -> int -> int
// ------------------------------------------

//-----------------------------------------------------------------------

// This is all right
[<AbstractClass>]
type AbstractClass1() = 
    abstract member Op : int -> int -> int

// This is all right
// Notice the getter and setter declaration
[<AbstractClass>]
type AbstractClass2() = 
    abstract AbstractMember: float with get, set

//-----------------------------------------------------------------------
// Default implementation of abtsract members
// By providing a default implementation for its abstract memebrs an 
// abstract class does no longer need to be decorated withe the attribute
// [<AbstractClass>] nor provide an implementation in a derived type.

type BaseClass2() = 
    abstract member F : unit -> unit
    default u.F() = 
        printf "F default implementation..."

//----------------------------------------------------------------------------

// One major difference between F# and C# is that in C# you can combine the 
// abstract definition and the default implementation into a single method, 
// using the virtual keyword. In F#, you cannot. You must declare the abstract 
// method and the default implementation separately. The abstract member has 
// the signature, and the default has the implementation.
type TwoParametersBaseClass(param1, param2) = 
    abstract member Compute: int with get
    default this.Compute = this.First + this.Second
    member this.First = param1
    member this.Second = param2

//-----------------------------------------------------------------------------

// overrides 

[<AbstractClass>]
type Animal() =
   abstract member MakeNoise: unit -> unit 

type Dog() =
   inherit Animal() 
   override this.MakeNoise () = printfn "woof"

//-----------------------------------------------------------------------------

// overrides with base invokation 

type Vehicle() =
   abstract member TopSpeed: unit -> int
   default this.TopSpeed() = 60

type Rocket() =
   inherit Vehicle() 
   override this.TopSpeed() = base.TopSpeed() * 10

//-----------------------------------------------------------------------------






   


