module LogXtreme.Utils.Test.ObjectMapper

open System
open Xunit
open Swensen.Unquote

open LogXtreme.Utils

// About class definition in F#
// https://fsharpforfunandprofit.com/posts/classes/
// https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/members/constructors
// https://stackoverflow.com/questions/27450581/making-a-class-with-a-private-constructor-in-f
// https://stackoverflow.com/questions/4923666/how-do-i-create-a-class-instance-and-fill-properties-in-f?rq=1

// About the declaration of members and properties on F# types 
// https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/members/properties

// The source type needs only read-only properties whose value is set by the constructor
type Source(name: string, code: int, date: DateTime) = 
    member thi.Name = name
    member this.Code = code
    member this.Date = date

// The target type can be populated by the mapper only when the type declares properties
// that have a public setter. In F# the basic member declaration is public readonly.
type Target() = 
    member val Id: string = System.String.Empty with get, set 
    member val Name: string = System.String.Empty with get, set
    member val Code: int = 0 with get, set
    member val Date: DateTime = System.DateTime.MinValue with get, set

// On using Expressions and Lambdas
// https://stackoverflow.com/questions/45896796/pass-expressions-and-lambdas-parameter-to-c-sharp-extension-method-in-f
// https://stackoverflow.com/questions/3238406/in-f-how-can-i-produce-an-expression-with-a-type-of-funcobj
[<Fact>]
let ``ObjectMapper.Populate maps property values from source to target``() = 

    // arrange 
    let name = @"name"
    let code = 123
    let date = new System.DateTime(1980,2,15)    
    let source = Source(name, code, date)
    let mapper = ObjectMapper<Source, Target>(source)  
    let createId = fun n (c: int) (d: DateTime) -> n+c.ToString()+d.ToString()
    let createIdFromSource = fun (s: Source) -> createId s.Name s.Code s.Date    
    let expectedId = createIdFromSource source

    // act     
    mapper.Populate((fun t -> t.Name), fun s -> s.Name) |> ignore
    mapper.Populate((fun t -> t.Code), fun s -> s.Code) |> ignore
    mapper.Populate((fun t -> t.Date), fun s -> s.Date) |> ignore    
    mapper.Populate((fun t -> t.Id), fun s -> createIdFromSource s) |> ignore

    // assert
    test<@ mapper.Target.Name = source.Name @>    
    test<@ mapper.Target.Code = source.Code @>    
    test<@ mapper.Target.Date = source.Date @>
    test<@ mapper.Target.Id = expectedId @>

