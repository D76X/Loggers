module public LogXtreme.ComputationExpressions.Examples.Continuation1

// In C# or other imperative languages we have something like 

// call a function ->
//    <- return from the function
// call another function ->
//    <- return from the function
// call yet another function ->
//    <- return from the function

// this is an “in” and “out” style
// that is we call a function passing some values to it and it retuen a vlue to the caller
// the callee is ignorant of what to do with the value it can only return it to the caller
// the caller then decides how to continue

// In functional paradigms it is more common to adhere to the CPS (Continuation Passing Style)

// evaluate something and pass it into ->
//    a function that evaluates something and passes it into ->
//       another function that evaluates something and passes it into ->
//          yet another function that evaluates something and passes it into ->
//             ...etc...

// In this Style the caller in addition to the normal parameters passed to the first function of 
// the chain  passes also one or more fuctions to use for the continuation of the chain of calls
// This can also be done in C# by declaring method whose signature encompasses delegate types!

// CPS and the keyword let [ read (=>) as "means"] and lambas

// let x = someExpression 
// => 
//let x = someExpression in [an expression involving x]
// =>
// someEpression |> (fun x -> [an expression involving x] )

// let x = 42
// let y = 43
// let z = x + y  

// can be read as 

// let x = 42 in   
//   let y = 43 in 
//     let z = x + y in
//        z    // the result

// or 

// 42 |> (fun x ->
//   43 |> (fun y -> 
//      x + y |> (fun z -> 
//        z)))

// or

// let pipeInto (someExpression,lambda) = someExpression |> lambda 

// pipeInto (42, fun x ->
//   pipeInto (43, fun y -> 
//     pipeInto (x + y, fun z -> 
//        z)))

// which is the same as

// pipeInto (42, fun x ->
// pipeInto (43, fun y -> 
// pipeInto (x + y, fun z -> 
// z)))

