module public LogXtreme.ComputationExpressions.Examples.ExampleTraceBuilder3WithYield

// https://fsharpforfunandprofit.com/posts/computation-expressions-builder-part1/
type TraceBuilderA() = 
    member this.Bind(m, f) = 
        match m with 
        | None -> 
            printfn "Binding with None. Exiting."
        | Some a -> 
            printfn "Binding with Some(%A). Continuing" a
        Option.bind f m

    member this.Return(x) = 
        printfn "Returning a unwrapped %A as an option" x
        Some x

    member this.ReturnFrom(m) = 
        printfn "Returning an option (%A) directly" m
        m

let traceA = TraceBuilderA()


// in order to use the yield keyword in a workflow the builder 
// must implement the Yield member function

// the following does not compile with the error 
// "This control construct may only be used if the computation expression builder 
// defines a 'Yield' method"
//------------------------------------------------------------------------------------
//traceA { 
//    yield 1 // USED IN A "sequence expression" to produce a value of a sequence
//    } |> printfn "Result for yield: %A" 
//------------------------------------------------------------------------------------

// what should the implementation of “yield” method look like?

