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

type TestClass(pValue: string) = 
    member val IdProperty: string = pValue with get, set

[<Fact>]
let ``Expression.ToPropertyInfo gets PropertyInfo for property on class instance``()=

    // arrange
    let expectedPropertyValue = @"testValue"
    let instance = TestClass(expectedPropertyValue) 
    let readPropertyValue (i: TestClass) = i.IdProperty     
    let lamba = fun() -> readPropertyValue       
    
    // how do I pull out the name of a property in F#?  
    // http://www.contactandcoil.com/software/dotnet/getting-a-property-name-as-a-string-in-f/
    let expectedPropertyName = "IdProperty" // YUK! 

    // act
    // (fun (i: TestClass) -> readPropertyValue i) is the expression for a lambda to read the value of a property
    // notice the brakets sourround the lambda expression as in (lamba) where lambda = fun (i: TestClass) -> readPropertyValue i
    let directReadBackPropertyValue = instance.IdProperty
    let readPropertyValueViaFunction  = readPropertyValue instance
    let readPropertyValueViaLambda  = lamba() instance    
    
    // this is wrong and throws the exception for Member = null
    //let propertyInfo = ExpressionExtensions.ToPropertyInfo((fun (i: TestClass) -> readPropertyValue i))
    

    // assert
    
    //Assert.True(propertyInfo :? System.Reflection.PropertyInfo) // MOOT POINT!
    Assert.Equal(expectedPropertyValue, directReadBackPropertyValue)
    Assert.Equal(expectedPropertyValue, instance.IdProperty)
    Assert.Equal(expectedPropertyValue, readPropertyValueViaFunction)
    Assert.Equal(expectedPropertyValue, readPropertyValueViaLambda)

    // this is the main test
    Assert.Equal(expectedPropertyName, propertyInfo.Name)
    
    Assert.True(false);

