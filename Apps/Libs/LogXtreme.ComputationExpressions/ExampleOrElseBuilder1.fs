module public LogXtreme.ComputationExpressions.Examples.OrElseBuilder1

// not all computation expression define the members Bind and Return
// this is a computation expression which defines the members Combine, Delay and RetunFrom
// Combine is similar to Bind in concept - at least in this specific example
// Delay is always implemented when Cobine is defined - more on Delay in other examples
// For now Delay does nothing so to speak
// Return is used at the end of teh computation expression to return a wrapped value
// ReturnFrom is used to retun the unwrapped value
// Cobine takes two wrapped values and cobines them to produce another wrapped value
// In this example Cobine is used to return a wrapped value that is either a or b
// depending on whether a is Some or None respectively.
type OrElseBuilder() =

    member this.Combine (a, b) =    
        match a with
        | Some _ -> a
        | None -> b

    member this.Delay(f) = f()
    member this.ReturnFrom(x) = x

// if a and b are expressions the builder obeve can be used to create a or-else workflow
let orElse = OrElseBuilder()

let map1 = [ ("1","One"); ("2","Two") ] |> Map.ofList
let map2 = [ ("A","Alice"); ("B","Bob") ] |> Map.ofList
let map3 = [ ("CA","California"); ("NY","New York") ] |> Map.ofList

// given a number of dictionaries try to find the value associated to a key
// in any
let multiLookUp key = orElse{
    return! map1.TryFind key
    return! map2.TryFind key
    return! map3.TryFind key
}

multiLookUp "A" |> printfn "Result for A is %A"
multiLookUp "CA" |> printfn "Result for CA is %A"
multiLookUp "X" |> printfn "Result for X is %A"