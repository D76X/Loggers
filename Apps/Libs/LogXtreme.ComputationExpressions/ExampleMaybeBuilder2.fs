module public LogXtreme.ComputationExpressions.Examples.MaybeBuilder2

// the same builder but the Bind member is implemeted by reusing the Option.Bind
type MaybeBuilder() = 
    member this.Bind(x, f) = Option.bind f x
    member this.Return(x) = Some(x)       

let maybe = MaybeBuilder()

let divideby bottom top = 
    if bottom = 0
    then None
    else Some(top/bottom)

let divideByWorkflow init x y z =
    maybe
        {
        let! a = init |> divideby x
        let! b = a |> divideby y
        let! c = b |> divideby z
        return c
        }

let good = divideByWorkflow 12 3 2 1
let bad = divideByWorkflow 12 3 0 1

