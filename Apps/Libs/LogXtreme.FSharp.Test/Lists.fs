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
            
// example of recursion on lists
// every head goes on the stack!
let rec sum1 list =
    match list with 
        | head::tail -> head + sum1 tail
        | [] -> 0
  
// better with accumulator to ease the clutter on the stack
// on long lists and avoid the possibility of stack overflow
// the accumulator is nothing more than some state captured 
// in the context of the function
let sum2 list =
    let rec loop list acc = 
        match list with 
            | head::tail -> loop tail (acc+head)
            | [] -> acc
    loop list 0

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

[<Fact>]
let ``test List.isEmpty List.exist(2)``()=

    // arrange
    // List.exist2 only works on lists of the same length
    let list1 = [0; 0; 1; 0]
    let list2 = [0; 0; 1; 0]    
    let list3 = [0; 0; 0; 0]      
    let list4 = ["one"; "two"; "three"];
    let list5 = ["one"; "owt"; "three"];
    let list6 = ["1";"2";"3"]
    let list7 = [1..3];

    // simplified function to reverse the string representation of any value
    // https://stackoverflow.com/questions/4556160/is-there-more-simple-or-beautiful-way-to-reverse-a-string
    let reverse str = new string(str.ToString().ToCharArray() |> Array.rev) 
    //...

    // act

    // assert
    test<@ List.isEmpty list1 = false @>
    test<@ List.isEmpty List.Empty = true @>
    test<@ List.exists (fun e -> e = 1) list1 = true @>
    test<@ List.exists2 (fun e1 e2 -> e1 = e2) list2 list3 = false @>
    //test<@ List.exists2 (fun e1 e2 -> e1 = reverse e2) list4 list5 = true @>

