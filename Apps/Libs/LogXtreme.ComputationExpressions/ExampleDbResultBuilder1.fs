module public LogXtreme.ComputationExpressions.Examples.DbResultBuilder1

// This is another example of builder with a Bind member 
// the wrapper type is no longer the F# Option type, instead a custon type is used
// which is similar in concept to the option type. The DbResult type is a 
// discriminated union type to define the concept of Success and Failure.

//--------------------------------------------------------------------------------------------------------
// Computation Expression Builders falls under 3 common patterns of the 
// corresponding workflows

// -1 Success/Failure ~ True/False ~ Binary Option (Some/None)

// -2 Sequencial processing/ sequencial evaluation of statements

// -3 Manipulation of datastructures 

//--------------------------------------------------------------------------------------------------------

type DbResult<'a> = 
    | Success of 'a
    | Error of string

// a computation expression builder type is defined on which workflows can be 
// constructed so that calls to a a database can be chained to each other in 
// order to form a query. Each call to teh database is follwed to another one 
// that makes used of some data produced by the previous query as its input.
// As usual a workflow implies chaining evaluation of expression and deciding
// what to do next on teh basis of teh outcome of the previous evaluation.
// Bind here takes a wrapped value m of type DbResult that is obtained by 
// evaluationg the previous expression in the chain and if m is an Error the 
// evaluation of the following expression is replaced by passing m itself
// along the chain of calls until the last link in the chain is reached and 
// Return is used to return teh value from the computation as wrapped value.
// When m is Success instead it unwrapped value is used as the input of the 
// following expression  
type DbResultBuilder() = 

    member this.Bind(m,f) = 
        match m with
        | Error _ -> m
        | Success a ->
            printfn "\tSucessful: %s" a
            f a
           
    member this.Return(x) = Success x