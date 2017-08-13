module public LogXtreme.ComputationExpressions.Examples.ExampleTraceBuilder2WithZero

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

// failing at compile time with "This value is not a function and cannot be applied"
// it doesn’t make sense to have nothing at all in a computation expression!
// it’s purpose is to chain expressions together - we need at least an expression.
//------------------------------------------------------------------------------------
//traceA {
// // This value is not a function and cannot be applied
//} |> printfn "Result for simple expression: %A" 
//------------------------------------------------------------------------------------

// again failing at compile time with 
// "This control construct may only be used if the computation expression builder defines 
// a 'Zero' method"

//------------------------------------------------------------------------------------
//traceA { 
//    printfn "hello world"
//    } |> printfn "Result for simple expression: %A"
//------------------------------------------------------------------------------------

// However the follwing is syntactically correct and works.
// As shown later implemeting the member fuction Zero for a builder is required only if the builder
// is employed to define a workflow that does not explicitely return a value such as via "return"
// or "yield".

traceA { 
    printfn "hello world"
    return 0
    } |> printfn "Result for simple expression: %A"

// ------------------------------------------------------------------------------------------------

// let's then define the Zero member function of the builder type to fix the problem at compile time
// but in addition to that we also want to understand why adding the Zero member function fixes the 
// problem.  

// This TraceBuilder type follows the pattern "sucess-or failure".
// This means that its Zero member function when implemented should return "failure".
// In this case "failure" is indicated by the value None of the type Option. 

// https://fsharpforfunandprofit.com/posts/computation-expressions-builder-part1/
type TraceBuilderWithZero() = 
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
     
    member this.Zero() = 
        printfn "Zero"
        None

let tracewz = TraceBuilderWithZero()

// this instance of teh builder type Bind expressions int o a workflow but does not retun anything
// (explicitly) in order for a workflow to be syntactically correct witjout a "return" or "return'"
// or "yield" the builder type must declare the member function Zero which must return a "wrapped"
// value.

// Which "wrapped value" should Zero return ?

// There are 3 general workflow cases
// 1- Workflow with success-or-failure style => Zero retunrs "failure".
// 2- Workflow for sequential processing => Zero returns a "wrapped unit"
//    for example Some () where Some is the wrapper around () that is Option is the wrapper type and ()
//    "the unit" is the value - notice that this is the same as Return ().
// 3- Workflow that manipulates datastructures => Zero return the "empty" data structure.
//    For example if the datastructure is of List type then Zero should retun the empty list []

tracewz { 
    printfn "hello world"
    } |> printfn "Result for simple expression: %A"

// another example is that of a workflow where "retun" may not always be evaluated
tracewz {
    if false then return 1
} |> printfn "Result for if without else: %A"  

