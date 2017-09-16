module LogXtreme.FSharp.Test.CodeQuotations

// https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/code-quotations
// https://stackoverflow.com/questions/2246206/what-is-the-equivalent-in-f-of-the-c-sharp-default-keyword 
// https://stackoverflow.com/questions/25788123/build-lambda-expression-programmatically-in-f
// https://stackoverflow.com/questions/8133860/code-quotations-and-expression-trees

open System
open System.Numerics
open Xunit
open Swensen.Unquote

// You need this namespace to use F# code quotations
open Microsoft.FSharp.Quotations

// There are several active patterns that can be used to analyze expression objects.
open Microsoft.FSharp.Quotations.Patterns
open Microsoft.FSharp.Quotations.DerivedPatterns

// F# code quotations 

// F# code quotations lets you generate an abstract syntax tree that represents F# code.
// This sounds very much like the role of the Expression class in C# and in fact there 
// are similarities. The abstract syntax tree can then be traversed and processed eg. to
// generate F# code or generate code in some other language. This is similar to what is 
// done in C# with LINQ to SQL where expressions are compiled to SQL.

// F# quoted expressions

// Quoted expression is an F# are expression delimited is a special way so that the F# 
// compiler doe not compile them as part of your program rather they are compiled into 
// an object that represents an F# expression. 

// Example - 1
// https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/code-quotations

// The following example illustrates the use of code quotations to put F# code into an 
// expression object and then print the F# code that represents the expression. 
// This example does not include all the possible patterns that might appear in an F# 
// expression. Any unrecognized pattern triggers a match to the wildcard pattern (_) 
// and is rendered by using the ToString method hich, on the Expr type, lets you know 
// the active pattern to add to your match expression.


