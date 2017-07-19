module LogXtreme.FSharp.Test.Equality

open Xunit
open System.Numerics;
open System

type UserDetails = {
    Id: Guid
    UserName: string
    Password: string
    JoinDate: DateTime}

(*structural equality in F#*)
[<Fact>]
let ``1 plus 2 gives 3``() = 
    Assert.Equal(3,1+2)

[<Fact>]
let ``2 plus 2 gives 4``() = 
    Assert.Equal(4,2+2)

[<Fact>]
let ``complex 1,j = complex 1,j``()= 
    let c1 = Complex(1.,1.)
    let c2 = Complex(1.,1.)
    let e = (c1 = c2)
    Assert.True(e)
    Assert.True((c1=c2))
    Assert.Equal(c1,c2)

[<Fact>]
let ``UserDetails user1 = user2``()= 

    let u1 = {
        Id = Guid "A738A7D1-8232-44FD-9296-852D856EE554"
        UserName = "user1"
        Password = "password1"
        JoinDate = DateTime.MinValue} 

    let u2 = {
        Id = Guid "A738A7D1-8232-44FD-9296-852D856EE554"
        UserName = "user1"
        Password = "password1"
        JoinDate = DateTime.MinValue} 

    Assert.True((u1=u2))

//type Class1() = 
//    member this.X = "F#"

//[<Fact>]
//let ``vector (1,j) = vector (1,j)`` = 
//    let v1 = Vector


