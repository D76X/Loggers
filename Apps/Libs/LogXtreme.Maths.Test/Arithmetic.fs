module LogXtreme.Maths.Test.Arithmetic

open Xunit
open LogXtreme.Maths.Arithmetic      

[<Fact>]
let ``1 plus 2 gives 3``() = 
    Assert.Equal(3,1+2)


[<Fact>]
let ``add 1 to 2 gives 3``() = 
    Assert.Equal(3,add 1 2)

[<Theory>]
[<InlineData(3, 5)>]
[<InlineData(7, -2.6)>]
let ``add x to y gives x + y`` x y =
    Assert.Equal(x+y, add x y)
