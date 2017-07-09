module public LogXtreme.Maths.Test.Arithmetic

open LogXtreme.Maths.Arithmetic
open Microsoft.VisualStudio.TestTools.UnitTesting

//[<TestMethod>]
//let ``When 2 is added to 2 expect 4``() = 
//    Assert.AreEqual(4, 2+2)

[<TestClass>]
type TestClass() = 

    [<TestMethod>]
    member this.``add 1 and 2 to get 3``()=
        Assert.Equals(3, 1+2)

