module public LogXtreme.ComputationExpressions.Examples.OrElseBuilder1

// not all computation expression define the members Bind and Return
// this is a computation expression which defines the members Combine, Delay and RetunFrom
// Combine is similar to Bind in concept - at least in this specific example
// Delay is always implemented when Cobine is defined - more on Delay in other examples
// For now Delay does nothing so to speak
// Return is used at the end of teh computation expression to return a wrapped value
// ReturnFrom is used to retun the unwrapped value
type OrElseBuilder() =

    member this.Combine (a, b) =    
        match a with
        | Some _ -> a
        | None -> b

    member this.Delay(f) = f()

    member ReturnFrom(x) = x

let orElse = OrElseBuilder()