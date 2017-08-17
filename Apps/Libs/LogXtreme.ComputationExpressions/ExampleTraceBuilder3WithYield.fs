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

// What should the implementation of “yield” method look like?
// Yield must have the same signature of Return ('T -> M<'T>)
// It must take an unwrapped value and return a wrapped value


type TraceBuilderB() = 
    member this.Bind(m, f) = 
        match m with 
        | None -> 
            printfn "Binding with None. Exiting."
        | Some a -> 
            printfn "Binding with Some(%A). Continuing" a
        Option.bind f m

    // example : return 1 => produces Some(1)
    member this.Return(x) = 
        printfn "Returning a unwrapped %A as an option (wrapped value)" x
        Some x

    // example : return! Some(1) => produces Some(1)
    member this.ReturnFrom(x) = 
        printfn "Returning an option (%A) directly" x
        x

    // example : yield 1 => produces Some(1)
    member this.Yield(x) = 
        printfn "Yielding an unwrapped (%A) as an option (wrapped) value"  x
        Some x

    // example : yield! Some 1 => produces Some (1)
    member this.YieldFrom(x) =
        printfn "Yielding an option (%A) directly"  x
        x        

let traceB = TraceBuilderB()

// this is the same as using return
traceB {
    yield 1
    } |> printfn "Result for yield: %A"

traceB {
    return 1
    } |> printfn "Result for return: %A"

// this is the same as return! Some 1
// for this to work 
traceB {
    yield! Some 1
    } |> printfn "Result for yield! : %A"

traceB {
    return! Some 1
    } |> printfn "Result for return! : %A"

