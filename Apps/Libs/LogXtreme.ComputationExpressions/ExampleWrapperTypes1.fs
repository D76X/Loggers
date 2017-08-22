module public LogXtreme.ComputationExpressions.Examples.ExampleWrapperTypes1

// https://fsharpforfunandprofit.com/posts/computation-expressions-wrapper-types/
// https://fsharpforfunandprofit.com/posts/computation-expressions-wrapper-types-part2/

// This is the classic example of builder used to construct workflows to chain
// success-failure expressions.
type MaybeBuilder() =
    member this.Bind(m, f) = Option.bind f m
    member this.Return(x) = Some x

let maybewf = MaybeBuilder()

//--------------------------------------------------------------------
// This is an example of the pattern for this kind of workflows

// let result = 
//     maybe 
//         {
//         let! anInt = expression of Option<int>
//         let! anInt2 = expression of Option<int>
//         return anInt + anInt2 
//         }    
//--------------------------------------------------------------------


// Here a practical example

// some function that returns an option
let divideBy den num = 
    if den = 0 
    then None
    else Some(num/den)

let result = 
    maybewf
        {
        let! anInt = 3 |> divideBy 3
        let! anInt2 = 8 |> divideBy 4
        return anInt + anInt2 
        }  

//-----------------------------------------------------------------------------------------------  
// In the simple case of the MaybeBuilder it is clear the the "wrapper" type is Option<T>
//-----------------------------------------------------------------------------------------------

// Other wrapper types are also possible.
// The following example illustrates a similar success-failure style builder and a corresponding
// success-failure type. It falls in the same pattern as the Maybe builder above but the "wrapper"
// type in no longer the native F# Option<T> (generic union), instead a custom DbResult<T> is used
// in the context of teh DbResultBuilder.

// our own specific generic "wrapper" type
type DbResult<'a> = 
    | Success of 'a
    | Error of string

// A builder of teh success-failure pattern that employs the BdResult<T> wrapper.
// This means that his builder can be used to chain expressions that have DbResult as their retun type.
// It is worth noting here due to the fact that the ad'hoc type DbResult is used as "wrapper" type the 
// the Bind implementation can no longer be based on the Option.bind as it was the case in the MaybeBuilder
// builder above. However, the semantic is the same.

type DbResultBuilder() = 

    // when f m returns Success then continue by applying teh next f to the result
    // when f m retunrns Error then interrupt the chain of calls and returns the Error
    member this.Bind(m,f)=
        match m with 
        | Error _ -> m
        | Success a ->
            printfn "\tSuccessful: %s" a
            f a