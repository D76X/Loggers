module public LogXtreme.ComputationExpressions.Examples.MaybeBuilder1

// A computation expression must implement at least two members - Bind and Return
// Bind takes a wrapped value and a continuation function
// The wrapper is the Option type in this case
// The continuation function oprates on the unwrapped type
// The return type of the continuation function must be a wrapped type - Option in this case
// The Return member must return a wrapped type
type MaybeBuilder()= 
    // This implementation says that if x is the option None then pass None on to the next 
    // call to Bind which will code all the successive calls to Bind in the computation to
    // themeselves pass None to the follwing until reaching the retrun statement of the 
    // computation.
    // If the match is Some value instead that unwrapped value is bound to the symbol used
    // as an input to teh follwing continuation.
    member this.Bind(x, f) = 
        match x with
        | None -> None
        | Some a -> f a

    // When the chain of calls of all the expressions in the computation expressions reaches the
    // last expression the last computed value is retuned as Some value (a value wrapped in a 
    // option type)
    member this.Return(x) = 
        Some(x)       

// use an instance of the builder to implement a workflow 
// (the "new" keyword in fron to the builder type is added by the compiler) 
let maybe = MaybeBuilder()

// to use this builder you need a function that takes an number and returns an option of some type
// for example in the follwing we attempt to compute the value top/bottom and if it is possible we
// return it wrapped in the the option Some. If we cannot compute top/bottom we return the option
// (wrapper) None
let divideby bottom top = 
    if bottom = 0
    then None
    else Some(top/bottom)

// We can use the MaybeBuilder to implement a divide by workflow because the divideby function has
// an option as its retun type so the Bind member of the builder on which the computation expression
// is based will know how to do the continuation calls.
let divideByWorkflow init x y z =
    maybe
        {
        let! a = init |> divideby x
        let! b = a |> divideby y
        let! c = b |> divideby z
        return c
        }

// this compiles to 
// evaluate divideby x init and bind it to a of the next call if it is Some a
// 
// Bind(divideby x init, fun a ->
// Bind(divideby y a   , fun b ->
// Bind(divideby z b   , fun c ->
// return' c ))

let good = divideByWorkflow 12 3 2 1
let bad = divideByWorkflow 12 3 0 1

