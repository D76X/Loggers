module LogXtreme.Maths.Test.Arithmetic

open Xunit
open LogXtreme.Maths.Arithmetic      

[<Fact>]
let ``add 1 to 2 gets 3``() = 
    Assert.Equal(3,1+2)
