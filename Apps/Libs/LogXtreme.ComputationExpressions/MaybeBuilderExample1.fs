module public LogXtreme.ComputationExpressions.MaybeBuilderExample1

// A computation expression must implement at least two members - Bind and Return
// Bind takes a wrapped value and a continuation function
// The wrapper is the Option type in this case
// The continuation function oprates on the unwrapped type
// The return type of the continuation function must be a wrapped type - Option in this case
// The Return member must return a wrapped type
type MaybeBuilder() = 

    // This implementation says that if x is the option None then pass None on to the next 
    // call to Bind 
    member this.Bind(x, f) = 
        match x with
        | None -> None
        | Some a -> f a

    // When the chain of calls of all the expressions in the computation expressions reaches the
    // last expression the last computed value is retuned 
    member this.Return(x) = 
        Some(x)


