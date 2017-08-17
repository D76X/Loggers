module public LogXtreme.ComputationExpressions.Examples.ExampleBuilderWithFor1

// https://fsharpforfunandprofit.com/posts/computation-expressions-wrapper-types-part2/

// List<T> or IEnumerable<T> can be used as wrapper types.
// But how can this be? 
// There is no one-to-one correspondence between the wrapper type and the unwrapped type!
// This is where the “wrapper type” analogy becomes a bit misleading.

//------------------------------------------------------------------------------------------
// Bind is a way of connecting the oputput of one expressin with the input of the following
// expression. Bind "unwraps" the value amd applies the continuation function to the "unwrapped"
// value.

// Some wrapped value W<x> 
// |> Bind 
// |> fun x -> some expression involving x which returns W(y)
// |> Bind
// |> fun y -> some expression involving y which returns W(z)
// |> etc.
//------------------------------------------------------------------------------------------

// Le's go back to the assertion List<T> or IEnumerable<T> can be used as wrapper types.
// We can define a function buind as follows 

//---------------------------------------------------------------------------------------
// given List as wrapper the type or bind can take a wrapped value that is a list, then
// "unwrap" the value and apply a function to it from which another list that is a warpped
// value is returned.

// For example 

//let bind(list,f) =
    // 1) for each element in list, apply f
    // 2) f will return a list for each input (as required by its signature)
    // 3) the result is a list of lists
//---------------------------------------------------------------------------------------

// https://fsharpforfunandprofit.com/posts/elevated-world/

// given a list of values list
// given a function f that given a value returns a list of values
// apply f to each of the value of list
// concatenate the resulting list (flatten the lists) to produce a result

let bind(list,f) =
    list 
    |> List.map f 
    |> List.concat

// Example 1  
let list1 = [1;2;3]
let fun1 x = [x + 1]
let res1 = bind(list1, fun1)

// Example 2
let list2 = [1;2;3]
let fun2 m  =
    if m % 2 = 0 then [m-1;m+1] 
    else [m*(-2);m*2] 

let res2 = bind(list2, fun2)

// Example 3

// This shows how the binf function above can be used in conjunction with the concept
// of continuation. It takes a value from the first list and create a list by summing 
// it to each value of the second list. The first list holds three values hence this 
// process produces three lists one for each of the values in the first list. Finally,
// the three list are concatenated to produce a single resulting list.  

let add = 
    bind( [1;2;3], fun elem1 -> 
    bind( [10;11;12;14], fun elem2 -> 
        [elem1 + elem2] // a list!
    ))