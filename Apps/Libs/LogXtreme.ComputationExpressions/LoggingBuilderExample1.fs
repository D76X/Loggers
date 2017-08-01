module public LogXtreme.ComputationExpressions.LoggingBuilderExample1

// an computation expression is a F# type 
// it must define at least the Bind and Return members
// the Bind member defines the continuation logic
// the Return member defines the return logic
type LoggingBuilder() = 
    let log p = printfn "expression is %A" p

    // in this Bind we say that before continuing by applying f to x
    // the side effect log x is caused bofore the continuation
    // f is the continuation function and x is the input that cames out from the last f x
    member this.Bind(x,f) = 
        log x 
        f x 

    // returns whatever the result of the last call in the chain of (f x) is
    member this.Return(x) = 
        x

// example of usage 

// given an instance of teh computation expression
// notce that we could have used "new" but the compiler warbs that it is redundant
let logger = LoggingBuilder()

// define a workflow
let loggedWorkflow = 
    logger
        {
        let! x = 1
        let! y = 2
        let! z = x+y
        return z
         }


// How does all this work?

// The Bind function: Bind 'b * ('b->'c) -> 'c
// https://fsharpforfunandprofit.com/posts/computation-expressions-bind/ 
// let! x = 43 in some expression => builder.Bind(43, (fun x -> some expression))
// Bind takes a tuple that is two parameters
// the first parameter is the expression (x = 43)
// the second is a lamba fun x -> some expression where x is bound in the expression used as first parameter (in this example)

// the let! is equivalent to Bind

// let! x = 1 
// let! y = 2 
// let! z = x+y

// is compiled to

// Bind(1, fun x -> 
// Bind(2, fun y -> 
// Bind(x+y, fun z ->...))

// notice that the argument are inverted between let! and the Bind