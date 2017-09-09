module LogXtreme.FSharp.Test.ExpressionsExtensions

// https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/functions/lambda-expressions-the-fun-keyword
// https://stackoverflow.com/questions/45896796/pass-expressions-and-lambdas-parameter-to-c-sharp-extension-method-in-f

open System
open System.Numerics
open Xunit
open Swensen.Unquote
open System.Linq.Expressions;
open LogXtreme.Extensions

open LogXtreme.Extensions

type TestClass() = 
    member val IdProperty: string = System.String.Empty with get, set

[<Fact>]
let ``Expression.ToPropertyInfo gets PropertyInfo for property on class instance``()=

    // arrange
    let instance = TestClass()     
    let readPropertyValue (i: TestClass) = i.IdProperty 
    let x = (fun() -> readPropertyValue)
    let propertyValue = readPropertyValue instance

    // act
    let propertyInfo = ExpressionExtensions.ToPropertyInfo(x)

    // assert
    Assert.True(false)

