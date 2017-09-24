module public LogXtreme.ComputationExpressions.Examples.ExampleWrapperTypes5

// https://fsharpforfunandprofit.com/posts/computation-expressions-wrapper-types-part2/

open System

// Wrapper types are normally generic types such as the F# native type Option<'T> and custome types
// such as DbResult<'T> which have similar semantic. However, wrapper types need not necessarly be 
// generic types.

// Can non-generic wrapper types work?
// Yes they can be used as shown in the following examples.
// However, using non generic tyep as wrapper types is not idiomatic in F# thus should be avoided.
// Cases with non generic wrappers are illustrated here only to better explain the mechanism behind
// Binding Expressions in F#.abs

// Example of Binding Expression in wich the wrapper type is string
// The unwrapped type is Int32 and the wrapped type in string

// https://stackoverflow.com/questions/2979178/f-string-format
// Notice that there is a differenc between printfn and sprintfn in F# as the latter returns a string
// while the former returns unit. String.Format may also be used in place of sprintfn non idiomatically.
type StringIntBuilder() = 
    member this.Bind(m, f) = 
        let b,i = System.Int32.TryParse(m)
        match b,i with
        | false,_ -> String.Format("error cannot parse {0} to Int32", m)
        | true,i -> f i

    member this.Return(x) =
        sprintf "%i" x

let stringintwf = StringIntBuilder()

// we may now do stuff with strings as if they were integers if possible! 
let goodStrToInt = stringintwf {
    let! f = "42"
    let! s = "43"
    return f+s 
} 

let badStrToInt = stringintwf {
    let! f = "42"
    let! s = "bad"
    return f+s
} 


// Can we use List<T> or IEnumerable<T> as wrapper types?
// Sure we can with the same provisos mentioned in relation to the previous example. 
// This exploration provides an oppotunity to deepen our understanding of the Bind function.
// member Bind : M<'T> * ('T -> M<'U>) -> M<'U>
// It is been assumed that Bind will unwrap M<something> and apply a function f:('T -> M<'U>)
// to something to return a value M<somethingelse>. However, this is not the whole story.
// What if M<something> is a value such as List<int>. The Bind function will unwrap the input 
// of type List<int> into something like int[] and apply the f to each of the integers in the
// unwrapped value. 

//let bind(list,f) =
    // 1) for each element in list, apply f
    // 2) f will return a list that is a wrapped type (as required by its signature)
    // 3) the result is a list of lists! => PROBLEM!

// PROBLEM!    
// which means that the “list of lists” is no good. We need to turn them back into a 
// simple “one-level” list i.e. by using List.concat.

// Finally 

// Bind applies the continuation function to each element of the passed in list, and then 
// flattens the resulting list of lists into a one-level list. List.collect is a library 
// function that does exactly that.

// Return converts from unwrapped to wrapped. In this case, that just means wrapping a single 
// element in a list.

type ListWorkflowBuilder() = 
    member this.Bind(list, f) = list |> List.collect
    member this.Return(x) = [x]