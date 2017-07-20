module LogXtreme.FSharp.Test.Equality

open System
open System.Numerics
open Xunit
open Swensen.Unquote

//type Class1() = 
//    member this.X = "F#"

type UserDetails = {
    Id: Guid
    UserName: string
    Password: string
    JoinDate: DateTime}

let user getId usr psw getDate = {
    Id = getId()
    UserName = usr
    Password = psw
    JoinDate = getDate()}

(*structural equality in F#*)
[<Fact>]
let ``1 plus 2 gives 3``() = 
    Assert.Equal(3,1+2)

[<Fact>]
let ``2 plus 2 gives 4``() = 
    test <@ (2+2) = 4 @>

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
        Id = Guid "DDB4F1D5-A694-477B-9667-7660E42BFE76"
        UserName = "user1"
        Password = "password1"
        JoinDate = DateTime.MinValue} 

    let u2 = {
        Id = Guid "DDB4F1D5-A694-477B-9667-7660E42BFE76"
        UserName = "user1"
        Password = "password1"
        JoinDate = DateTime.MinValue} 

    Assert.True((u1=u2))

[<Theory>]
[<InlineData("AF541848-DFA4-447A-9117-C088EDD04111", "user1", "psw1", "10/10/2000")>]
[<InlineData("BF19CD90-5423-471E-8EAD-0B1193642DCA", "user2", "psw2", "30/06/2010")>]
let ``user returs the correct result``(id: string) (usr: string) (psw: string) (joindate: string) =
    let getId = Guid
    let actual = user 
