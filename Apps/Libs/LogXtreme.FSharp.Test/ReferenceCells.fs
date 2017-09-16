module LogXtreme.FSharp.Test.ReferenceCells

// https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/reference-cells

open System
open System.Numerics
open Xunit
open Swensen.Unquote

// Reference cells are storage locations that enable you to create mutable values 
// with reference semantics. You use the ref operator before a value to create a 
// new reference cell that encapsulates the value. You can then change the underlying 
// value because it is mutable.

// A reference cell holds an actual value; it is not just an address.
// When you create a reference cell by using the ref operator, you create a copy of 
// the underlying value as an encapsulated mutable value.

