module LogXtreme.FSharp.Test.CodeQuotations

// https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/code-quotations
// https://stackoverflow.com/questions/2246206/what-is-the-equivalent-in-f-of-the-c-sharp-default-keyword 
// https://stackoverflow.com/questions/25788123/build-lambda-expression-programmatically-in-f
// https://stackoverflow.com/questions/8133860/code-quotations-and-expression-trees

// Quotations.Expr Class
// https://msdn.microsoft.com/visualfsharpdocs/conceptual/quotations.expr-class-%5bfsharp%5d

// Microsoft.FSharp.Quotations Namespace (F#)
// https://msdn.microsoft.com/visualfsharpdocs/conceptual/microsoft.fsharp.quotations-namespace-%5bfsharp%5d

open System
open System.Numerics
open Xunit
open Swensen.Unquote

// You need this namespace to use F# code quotations
open Microsoft.FSharp.Quotations

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

