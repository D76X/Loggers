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
    maybewf {
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
    // when f m retunrns Error then inte rrupt the chain of calls and returns the Error
    member this.Bind(m,f)=
        match m with 
        | Error _ -> m
        | Success a ->
            printfn "\tSuccessful: %s" a
            f a

    // when the last f m is reaches the return wraps x back
    // this suggests that between the last f m and the call to return something
    // else happens that unwraps the value from the last f m in the chain 
    member this.Return(x) = 
        Success x    

let dbresultwf = DbResultBuilder()

// with such a computation expression builder we could chain calles to services 
// where each may return a failure or a success and so that any failure in the 
// chain shoukd result in teh failure of teh workflow with a message related to
// the link where teh failure happened 
let getCustomerId name =
    if (name = "") 
    then Error "getCustomerId failed"
    else Success "Cust42"

let getLastOrderForCustomer custId =
    if (custId = "") 
    then Error "getLastOrderForCustomer failed"
    else Success "Order123"

let getLastProductForOrder orderId =
    if (orderId  = "") 
    then Error "getLastProductForOrder failed"
    else Success "Product456"

// and finally the workflow that chains these calls to services
let product = 
    dbresultwf {
    let! custId = getCustomerId "Alice"
    let! orderId = getLastOrderForCustomer custId
    let! productId = getLastProductForOrder orderId 
    printfn "Product is %s" productId
    return productId   
    } |> printfn "%A"
  
// or for the failing case
let productNotFound = 
    dbresultwf {
    let! custId = getCustomerId "Alice"
    let! orderId = getLastOrderForCustomer "" // cause an error in the call chain 
    let! productId = getLastProductForOrder orderId 
    printfn "Product is %s" productId
    return productId   
    } |> printfn "%A"

