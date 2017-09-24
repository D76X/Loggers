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



