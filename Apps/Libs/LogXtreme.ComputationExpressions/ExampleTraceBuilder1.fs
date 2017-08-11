module public LogXtreme.ComputationExpressions.Examples.ExampleTraceBuilder1

// https://fsharpforfunandprofit.com/posts/computation-expressions-builder-part1/
type TraceBuilder1() = 
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

let trace = TraceBuilder1()

// Exercise Return
trace { 
    return 1
    } |> printfn "Result 1: %A" 

// Exercise ReturnFrom
trace { 
    return! Some 2
    } |> printfn "Result 2: %A"

// Exercise Bind and Return
// you should read the let! as in the following
// {| let! pattern = expr in cexpr |}
// Bind(expr, (fun pattern -> {| cexpr |}))
// Bind the evaluation expr=(Some 1) to x with x the symbol in some expression cexpr containing the pattern x
trace { 
    let! x = Some 1 //Bind(Some1, fun x-> 
    let! y = Some 2 //Bind(Some2, fun y->
    return x + y    //x+y))
    } |> printfn "Result 3: %A" 

// Exercise the Bind with interrupted continuation
// the whole CE evaluates to <null>
trace { 
    let! x = None   //this will prevent the continuation through Bind
    let! y = Some 1
    return x + y
    } |> printfn "Result 4: %A" 

//do! is the same as let! 
//but is used to Bind expressions with <null>
trace { 
    do! Some (printfn "...expression that returns unit")
    do! Some (printfn "...another expression that returns unit")
    let! x = Some (1)
    return x
    } |> printfn "Result from do: %A" 

// failing 

trace { 
    printfn "hello world"
    } |> printfn "Result for simple expression: %A" 

