module public LogXtreme.ComputationExpressions.Examples.ExampleWrapperTypes2

// https://fsharpforfunandprofit.com/posts/computation-expressions-wrapper-types/
// https://fsharpforfunandprofit.com/posts/computation-expressions-wrapper-types-part2/

// This example is intended to show that the signature of the member Bind of a Computation Expression
// member Bind : M<'T> * ('T -> M<'U>) -> M<'U>
// does by no means imply that the type T must match the type U on the contrary!
// For example if the wrapper type is Option it is perfectly possible for  
type DbResult<'a> = 
    | Success of 'a
    | Error of string


type DbResultBuilder() = 

    member this.Bind(m,f)=
        match m with 
        | Error _ -> m
        | Success a ->
            printfn "\tSuccessful: %s" a
            f a

    member this.Return(x) = 
        Success x    

let dbresultwf = DbResultBuilder()

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

