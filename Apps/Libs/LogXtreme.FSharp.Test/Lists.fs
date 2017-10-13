module LogXtreme.FSharp.Test.Lists

// https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/lists
// https://msdn.microsoft.com/visualfsharpdocs/conceptual/collections.list-module-%5bfsharp%5d
// https://fsharpforfunandprofit.com/posts/tuples/

open Xunit
open Swensen.Unquote

// emulates the work of List.head and List.tail
// https://stackoverflow.com/questions/2246206/what-is-the-equivalent-in-f-of-the-c-sharp-default-keyword
let splitHeadTail<'a> (list: 'a list) =      
        match list with 
            | head::tail -> (head, tail)
            | _ -> (Unchecked.defaultof<'a>, [])  
    

[<Fact>]
let ``List.head List.tail give head & tail of a list``()=

    // arrrange
    // illustrates the usage of the List operators :: and @ 
    let headval = 1
    let head = [headval]
    let tail = 2 :: [3; 4; 5; 6;]
    let list = head @ tail

    // act 
    let (hm,tm) = list |> splitHeadTail 
    let hl = list |> List.head
    let tl = list |> List.tail

    // assert
    test<@ hm = headval @>
    test<@ hl = hm @>
    test<@ tm = tail @>
    test<@ tl = tm @>