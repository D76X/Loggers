module LogXtreme.Maths.Test.Arithmetic

open Xunit
open LogXtreme.Maths.Arithmetic

[<Theory>]
[<InlineData(3, 5)>]
[<InlineData(7, -2.6)>]
let ``add x to y gives x + y`` x y =
    Assert.Equal(x+y, add x y)


