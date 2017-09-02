module public LogXtreme.ComputationExpressions.Examples.ExampleWrapperTypes3

// This is the exact copy of the code from the paragraph "Bind and Return and wrapper types"
// at the URL below
// https://fsharpforfunandprofit.com/posts/computation-expressions-wrapper-types/

// This code is here for reference and is discussed in more detail in the example 
// ExampleWrapperType4

type DbResult<'a> = 
    | Success of 'a
    | Error of string

type CustomerId =  CustomerId of string
type OrderId =  OrderId of int
type ProductId =  ProductId of string

let getCustomerId name =
    if (name = "") 
    then Error "getCustomerId failed"
    else Success (CustomerId "Cust42")

let getLastOrderForCustomer (CustomerId custId) =
    if (custId = "") 
    then Error "getLastOrderForCustomer failed"
    else Success (OrderId 123)

let getLastProductForOrder (OrderId orderId) =
    if (orderId  = 0) 
    then Error "getLastProductForOrder failed"
    else Success (ProductId "Product456")

type DbResultBuilder() =

    member this.Bind(m, f) = 
        match m with
        | Error e -> Error e
        | Success a -> 
            printfn "\tSuccessful: %A" a
            f a

    member this.Return(x) = 
        Success x

let dbresult = new DbResultBuilder()

let product' = 
    dbresult {
        let! custId = getCustomerId "Alice"
        let! orderId = getLastOrderForCustomer custId
        let! productId = getLastProductForOrder orderId 
        printfn "Product is %A" productId
        return productId
        }
printfn "%A" product'