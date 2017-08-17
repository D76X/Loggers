

// https://fsharpforfunandprofit.com/posts/elevated-world/

// ---------------------------------------------------------------------------------------
// List.map
// https://msdn.microsoft.com/en-us/visualfsharpdocs/conceptual/list.map%5B't,'u%5D-function-%5Bfsharp%5D

// Signature:
//List.map : ('T -> 'U) -> 'T list -> 'U list
// Usage:
//List.map mapping list

//----------------------------------------------------------------------------------------

// These are straightforward examples of applications of List.map for conversion of 
// lists of values into other list of values of the same type or a different type.

let data = [1;2;3;4]
let r1 = data |> List.map (fun x -> x + 1)
let r2 = data |> List.map string
let r3 = data |> List.map (fun x -> (x,x))

let dates = [(1,1,2001); (2,2,2004); (6,17,2009)]
let list1 =
    dates |> List.map (fun (a,b,c) -> 
        let date = new System.DateTime(c, a, b)
        date.ToString("F"))

//----------------------------------------------------------------------------------------

// https://fsharpforfunandprofit.com/posts/elevated-world/

// This is a more sophisticated example in which the semantic of the concept of "map" 
// function is explored. In a few words a map functions can be described as a function 
// that has the signature below.

// E<a> -> (a->b) -> E<b>

// It takes some "E-value" E<a> from a set_E which is mapped 1:1 to a set_e then using this
// 1:1 correspondence it applies the function (a->b) to obtain the value b from the set_e
// and then it returns E<b> from the set_E which correspond to b in 1:1 map between set_e
// and set_E.   

// Example 1



//----------------------------------------------------------------------------------------