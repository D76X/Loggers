module LogXtreme.FSharp.Test.ExpressionsExtensions

// https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/functions/lambda-expressions-the-fun-keyword
// https://stackoverflow.com/questions/45896796/pass-expressions-and-lambdas-parameter-to-c-sharp-extension-method-in-f

open System
open System.Numerics
open Xunit
open Swensen.Unquote

open System.Linq.Expressions;
open LogXtreme.Extensions
open LogXtreme.FSharp.UtilsFunctions

type TestClass(pValue: string) = 
    member val IdProperty: string = pValue with get, set

[<Fact>]
let ``Expression.ToPropertyInfo and Expression.ToPropertyName work correctly``()=

    // arrange
    let expectedPropertyValue = @"testValue"
    let instance = TestClass(expectedPropertyValue) 
    // different ways of readinf the value of a property of a class instance
    let readPropertyValue (i: TestClass) = i.IdProperty     
    let lamba = fun() -> readPropertyValue
    let pValue = fun() -> instance.IdProperty
    
    // how do I pull out the name of a property in F#?  
    
    // this is teh crudest way possible
    let expectedPropertyNameRaw = @"IdProperty" // YUK! 
    
    // this is the way that is done in F# 
    // http://www.contactandcoil.com/software/dotnet/getting-a-property-name-as-a-string-in-f/
    let expectedPropertyName = propertyName <@ instance.IdProperty @>

    // act
    
    // read the property value in all possible ways
    let directReadBackPropertyValue = instance.IdProperty
    let readPropertyValueViaFunction  = readPropertyValue instance
    let readPropertyValueViaLambda  = lamba() instance
    let read_pValue = pValue()
    
    // exercise the extension method ExpressionExtensions.ToPropertyInfo
    let actualPropertyInfo = ExpressionExtensions.ToPropertyInfo(fun (i: TestClass) -> i.IdProperty)   
    
    // exercise the extension method ExpressionExtensions.ToPropertName
    let actualPropertyName = ExpressionExtensions.ToPropertyName(fun (i: TestClass) -> i.IdProperty)   
        
    // test that the different ways of reading the value of a property produce the expected result
    test<@ expectedPropertyValue = directReadBackPropertyValue @>
    test<@ expectedPropertyValue = instance.IdProperty @>
    test<@ expectedPropertyValue = readPropertyValueViaFunction @>
    test<@ expectedPropertyValue = readPropertyValueViaLambda @>
    test<@ expectedPropertyValue = read_pValue @>

    // test that the propertyInfo instace from the extension method call has Name set to
    // the expected property name of the class the property is defined on
    test<@ expectedPropertyNameRaw = expectedPropertyName @>
    test<@ expectedPropertyName = actualPropertyInfo.Name @>
    test<@ expectedPropertyName = actualPropertyName @>
    

