module LogXreme.Extensions.Test.EnumExtensions

open System
open Xunit
open Swensen.Unquote
open LogXtreme.Extensions

// Enumerations in F# 
// https://fsharpforfunandprofit.com/posts/enum-types/

// In F# enumerations are types declared as ordinary unions.
// In F# values of an enumerations must be qualified with the type of the enumeration.

// However there are some differences between simple unions and enumerations.
type TestColorUnion = Red | Green | Blue // this is a union
type TestColorEnum = Red = 1 | Green = 2 | Blue = 3
type TestSizeEnum = Small = 1 | Medium =2 | Large = 3

// Unions must declare cases with names whose first letter is uppercase.
// Enums can declare cases whose first letter is lowercase.
type TestEnumQuirk = Right = 1 | wrong = 2
//type WrongUnionDeclaration = Right | wrong

//F# allow for bitwise enumerations using the appropriate attribute
[<System.FlagsAttribute>]
type TestPermissionEnum = Read = 1 | Write = 2 | Execute = 4
let permission1 = TestPermissionEnum.Read ||| TestPermissionEnum.Write 

// Enumeration type with values declaring a description with attribute
let lineDescription: string = @"A Line"
let squareDescription: string = @"A Square"
let circleDescription: string = @"A Circle"
type TestGeometryEnum =     
    | [<System.ComponentModel.DescriptionAttribute("A Line")>] Line = 1 
    | [<System.ComponentModel.DescriptionAttribute("A Square")>] Square = 2 
    | [<System.ComponentModel.DescriptionAttribute("A Circle")>] Circle = 3

// Test Function
// TestGetEnumName pairs with the extension method Enum.GetName
let TestGetEnumName (enumValue: Enum) = System.Enum.GetName(enumValue.GetType(), enumValue)

// Test Function
// TestGetEnumDescription pairs with the extension method Enum.GetDescription
let TestGetEnumDescription (enumValue: Enum) =

    let enumValueName = enumValue.GetName()
    let fieldInfo = enumValue.GetType().GetField(enumValueName)
    let descAttrs = fieldInfo.GetCustomAttributes(typeof<System.ComponentModel.DescriptionAttribute>,false)    
    
    // https://stackoverflow.com/questions/2784281/firstordefault-in-f/25034371
    // this is semantically preferable to System.Linq IEnumerable<T>.FirstOrDefault()
    let descAttr = Seq.tryFind (fun _ -> true) descAttrs
    
    // https://msdn.microsoft.com/en-us/visualfsharpdocs/conceptual/casting-and-conversions-%5Bfsharp%5D
    // https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/casting-and-conversions
    let result = 
        match descAttr with 
        | Some a -> 
            let d = a :?> System.ComponentModel.DescriptionAttribute
            d.Description
        | None -> enumValueName

    result

[<Fact>]
let ``TestGetDescription returns expected enumeration value description``() = 

    // arrange 
    let geometryEnumValue = TestGeometryEnum.Square
    let expectedGeometryDesc =  squareDescription
    
    let colorEnumValue = TestColorEnum.Blue
    let expectedColorDesc = @"Blue" 

    // act 
    let actualGeometryDesc = TestGetEnumDescription geometryEnumValue
    let actualColorDesc = TestGetEnumDescription colorEnumValue

    // assert
    test<@ actualGeometryDesc = expectedGeometryDesc @>
    test<@ actualColorDesc = expectedColorDesc @>

[<Fact>]
let ``Enum.GetName extension method returns the name of the enum value as a string``() =

    // arrange
    let redValue = TestColorEnum.Red
    let expected = @"Red"

    // act
    let actual = redValue.GetName()

    // assert
    test <@ actual = expected @>


[<Fact>]
let ``Enum.GetName extension method matches F# implemetation output`` () =

    // arrange 
    let sizeValue = TestSizeEnum.Small
    let expected  = TestGetEnumName sizeValue

    // act 
    let actual = sizeValue.GetName()

    // assert
    test <@ actual = expected @>

[<Fact>]
let ``Enum.GetDescription extension method returns enumeration value description``() =

    // arrange 
    let expected = lineDescription
    let lineEnumVal = TestGeometryEnum.Line

    // act 
    let actual = lineEnumVal.GetDescription()

    // assert
    test<@ actual = expected @>

[<Fact>]
let ``Enum.GetDescription extension method returns Enum.GetValue for Enum value with no description``() =

    // arrange
    let redValue = TestColorEnum.Red
    let expected = redValue.GetName()

    // act
    let actual = redValue.GetDescription()

    // assert
    test <@ actual = expected @>

[<Fact>]
let ``Enum.GetDescription extension method matches F# implemetation output``() = 

    // arrange 
    let circleEnumValue = TestGeometryEnum.Circle
    let largeEnumValue = TestSizeEnum.Large
    let expectedGeometryDescription = TestGetEnumDescription circleEnumValue
    let expectedSizeDescription = TestGetEnumDescription largeEnumValue

    // act 
    let actualCircleEnumDesc = circleEnumValue.GetDescription()
    let actualSizeEnumDesc = largeEnumValue.GetDescription()

    // assert
    test<@ actualCircleEnumDesc = expectedGeometryDescription @>
    test<@ actualSizeEnumDesc = expectedSizeDescription @>