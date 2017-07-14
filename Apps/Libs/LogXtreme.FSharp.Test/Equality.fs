module LogXtreme.FSharp.Test.Equality

open Xunit
open System.Numerics;

(*structural equality in F#*)
[<Fact>]
let ``1 plus 2 gives 3``() = 
    Assert.Equal(3,1+2)

[<Fact>]
let ``complex (1,j) = complex (1,j)`` = 
    let c1 = Complex(1.,1.)
    let c2 = Complex(1.,1.)
    Assert.Equal(c1,c2)

type Class1() = 
    member this.X = "F#"
