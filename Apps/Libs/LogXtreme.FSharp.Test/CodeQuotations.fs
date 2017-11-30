module LogXtreme.FSharp.Test.CodeQuotations

// https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/code-quotations
// https://stackoverflow.com/questions/2246206/what-is-the-equivalent-in-f-of-the-c-sharp-default-keyword 
// https://stackoverflow.com/questions/25788123/build-lambda-expression-programmatically-in-f
// https://stackoverflow.com/questions/8133860/code-quotations-and-expression-trees

// Quotations.Expr Class
// https://msdn.microsoft.com/visualfsharpdocs/conceptual/quotations.expr-class-%5bfsharp%5d

// Microsoft.FSharp.Quotations Namespace (F#)
// https://msdn.microsoft.com/visualfsharpdocs/conceptual/microsoft.fsharp.quotations-namespace-%5bfsharp%5d

// Functions as First-Class Values
// https://docs.microsoft.com/en-us/dotnet/fsharp/introduction-to-functional-programming/functions-as-first-class-values

open System
open System.Numerics
open Xunit
open Swensen.Unquote

//-------------------------------------------------------
// You need this namespace to use F# code quotations
open Microsoft.FSharp.Quotations
// and offen you will need also the following
open Microsoft.FSharp.Quotations.Patterns
open Microsoft.FSharp.Quotations.DerivedPatterns
open Microsoft.FSharp.Quotations.ExprShape
//-------------------------------------------------------


// There are several active patterns that can be used to analyze expression objects.
open Microsoft.FSharp.Quotations.Patterns
open Microsoft.FSharp.Quotations.DerivedPatterns

// -------------------------
// F# code quotations 
// -------------------------

// F# code quotations lets you generate an abstract syntax tree that represents F# code.
// This sounds very much like the role of the Expression class in C# and in fact there 
// are similarities. The abstract syntax tree can then be traversed and processed eg. to
// generate F# code or generate code in some other language. This is similar to what is 
// done in C# with LINQ to SQL where expressions are compiled to SQL.

// -------------------------
// F# quoted expressions
// -------------------------

// Quoted expression is an F# are expression delimited is a special way so that the F# 
// compiler does not compile them as part of your program rather they are compiled into 
// an object that represents an F# expression. 

// You can mark a quoted expression in one of two ways.

//--------------------------------------------------------------------------------------
// 1- Quoted expression with type information <@...expression...@>

// A typed code quotation.
// let expr : Expr<int> = <@ 1 + 1 @>

// The type of a quoted expression with the typed symbols is Expr<'T>. The type parameter 
// T has is infered determined by the F# compiler. 

//--------------------------------------------------------------------------------------

//--------------------------------------------------------------------------------------
// 2- Quoted expression without type information <@@...expression...@@>

// An untyped code quotation.
// let expr2 : Expr = <@@ 1 + 1 @@>

// The type of the quoted expression without type information is the non-generic type 
// Expr. 

//--------------------------------------------------------------------------------------

//--------------------------------------------------------------------------------------

// 3- Splicing Operators

// The operators % and %% can be used to combine literal code quotations with expressions  
// created programmatically or from another code quotation. Both operators are unary prefix 
// operators.

// the %  operator is to insert a typed expression object into a typed quotation.
// the %% operator is to insert an untyped expression object into an untyped quotation.

// Example : expr is an untyped expression of type Expr.
// <@@ 1 + %%expr @@>

// Example : expr is a typed quotation of type Expr<int>.
// <@ 1 + %expr @>

//--------------------------------------------------------------------------------------

//--------------------------------------------------------------------------------------

// Fact 1
// Traversing a large expression tree is faster if you DO NOT include type information. 

// Fact 2
// There are a variety of static methods that allow you to generate F# expression objects 
// programmatically in the Expr class without using quoted expressions.

// Fact 3
// You can call the Raw property on the typed Expr class to obtain the untyped Expr 
// object.

// Fact 4
// To use code quotations, you must add an import declaration (by using the open keyword) 
// that opens the *** Microsoft.FSharp.Quotations *** namespace.

// Fact 5
// The F# PowerPack provides support for evaluating and executing F# expression objects.

//--------------------------------------------------------------------------------------


// println is a custom function that is contrived to display a F# expression object of type
// Expr in a friendly format. Internally it makes use of the recusive function print. It tries
// to match its input bound to the symbol expr using some of the active patterns available in 
// the namespaces below. There are however many more active patterns avaible in these two modules
// in addition to those used in this example.
//------------------------------------------------
// Microsoft.FSharp.Quotations.Patterns
// Microsoft.FSharp.Quotations.DerivedPatterns
//------------------------------------------------
// In this example all that is done with the expressions is to translate them into a string that
// is ultimately printed. However, other possibilities are open such as transalation of F# contructs
// into another language i.e. SQL or another .NET language or not. In this example the last branch
// of the match expression maps _ (anything) to expr.ToString().


//let println expr = 
//    let rec print expr = 
//        match expr with 
//        | Application(expr1, expr2) ->
//            // https://msdn.microsoft.com/en-us/visualfsharpdocs/conceptual/patterns.application-active-pattern-%5Bfsharp%5D
//            // https://docs.microsoft.com/en-us/dotnet/fsharp/introduction-to-functional-programming/functions-as-first-class-values
//            // Function application
//            print expr1
//            printf " "
//            print expr2
//        | SpecificCall <@@ (+) @@> (_,_,exprList) ->
//            // https://msdn.microsoft.com/en-us/visualfsharpdocs/conceptual/derivedpatterns.specificcall-active-pattern-%5Bfsharp%5D
//            // Matches a call to (+). Must appear before Call pattern below.            
//            print exprList.Head
//            printf " + "
//            print exprList.Tail.Head    
//        | Call(exprOpt, methodInfo, exprList) ->
//        | _ -> 
//            // for anything unmatched...
//            printf "%s" (expr.ToString())
//    print expr
//    printfn "%s" String.Empty

//How to get the name of function argument in F#?

//https://stackoverflow.com/questions/29599456/how-to-get-the-name-of-function-argument-in-f

//[<Fact>]
//let ``some quoted expression test``() =

    // arrange

    // act

    // assert


    
    
// The following example illustrates the use of code quotations to put F# code into an 
// expression object and then print the F# code that represents the expression. 

//--------------------------------------------------------------------------------------
// F# Code Quotations and generic functions

// Inner generic functions are not permitted in quoted expressions. 
// Consider adding some type constraints until this function is no longer 
// generic.

// https://stackoverflow.com/questions/26710818/f-code-quotations-and-generic-functions
//--------------------------------------------------------------------------------------
//...

// This example does not include all the possible patterns that might appear in an F# 
// expression. Any unrecognized pattern triggers a match to the wildcard pattern (_) 
// and is rendered by using the ToString method which, on the Expr type, lets you know 
// the active pattern to add to your match expression.

