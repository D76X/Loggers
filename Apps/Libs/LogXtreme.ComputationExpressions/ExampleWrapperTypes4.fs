module public LogXtreme.ComputationExpressions.Examples.ExampleWrapperTypes4

// https://fsharpforfunandprofit.com/posts/computation-expressions-wrapper-types/
// https://fsharpforfunandprofit.com/posts/computation-expressions-wrapper-types-part2/

// This example is intended to show that the signature of the member Bind of a Computation Expression
// member Bind : M<'T> * ('T -> M<'U>) -> M<'U>
// Here we focus on another aspect of the signature of the Bind member.
// When Bind chains the expressions in the computation each is fed with a wrapped value of type M<'T> 
// and returns another wrapped value M<'U>. The only constrain is that the wrapper type remains the same
// for all the expressions in the computations, that is each expression must evaluate to M<something>.
// The types 'T and 'U in the signature may change within a single expression and between the expressions 
// in the computation as the following example illustrates.


type DbResult<'a> = 
    | Success of 'a
    | Error of string

// let's declare three different types 
type CustomerId = CustomerId of string
type OrderId =  OrderId of string
type ProductId =  ProductId of string

// and some functions that may be chained together in a computation expression based on a builder 
// with the pattern success-failure. Notice that each function has a different output type that 
// is DbResult<CustomerId>, DbResult<OrderId> and DbResult<ProductIdId>. However the wrapper type
// DbResult is the same. This allows Bind to unwrap the output of one expression to provide the 
// input to the following expression. Moreover, the expressions in the chain will have to be arranged
// so that the unwrapped value from the output of the preceding ecpression id the tyep of the input 
// of the following expressions, that is 

// string -> DbResult<CustomerId>
// DbResult<CustomerId> -> CustomerId       // this is the unwrapping done by Bind
// CustomerId -> DbResult<OrderId>
// DbResult<OrderId> -> OrderId             // this is the unwrapping done by Bind
// OrderId -> DbResult<ProductId>

let getCustomerId name =
    if (name = "") 
    then Error "getCustomerId failed"
    else Success (CustomerId "Cust42")

let getLastOrderForCustomer (CustomerId custId) =
    if (custId = "") 
    then Error "getLastOrderForCustomer failed"
    else Success (OrderId "Order123")

let getLastProductForOrder (OrderId orderId) =
    if (orderId  = "") 
    then Error "getLastProductForOrder failed"
    else Success (ProductId "Product456")

// The builder's Bind declares the wrapper type M which in this case is DbResult<'a>.
// As long as the expressions in the compuations evaluates to DbResult<anytype> they can be chained.
// Another requirement is of course that the output of an expressions in the chain must be compatible
// with the input of the following expression in the chain.

// There are also other subtletises to watch out for.  
// The Bind expression must have the following signature member Bind : M<'T> * ('T -> M<'U>) -> M<'U>
// 'U != 'T
type DbResultBuilder() = 

    member this.Bind(m,f)=
        match m with 
        // if there is and error value in DbResult<CustomerId> rewrap it as error value of type DbResult<OrderId> 
        | Error e -> Error e 
        // notice that we must use A% instead of s% so that the type of a is not inferred to be string
        | Success a ->
            printfn "\tSuccessful: %A" a 
            f a

    member this.Return(x) = 
        Success x    

let dbresultwf = DbResultBuilder()


// and finally the workflow that chains these calls to services
let product = 
    dbresultwf {   
    let! custId = getCustomerId "Alice"    
    let! orderId = getLastOrderForCustomer custId
    let! productId = getLastProductForOrder orderId 
    printfn "Product is %A" productId
    return productId   
    } |> printfn "%A"
  
// or for the failing case
let productNotFound = 
    dbresultwf {
    let! custId = getCustomerId "Alice"
    let  wrongCustId = (CustomerId "") // replace to cause an error
    let! orderId = getLastOrderForCustomer wrongCustId // cause an error in the call chain 
    let! productId = getLastProductForOrder orderId 
    printfn "Product is %A" productId
    return productId   
    } |> printfn "%A"

