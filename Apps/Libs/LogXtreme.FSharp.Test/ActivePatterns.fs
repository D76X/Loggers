module LogXtreme.FSharp.Test.ActivePatterns

// https://fsharpforfunandprofit.com/posts/convenience-active-patterns/
// https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/active-patterns

// https://stackoverflow.com/questions/3151099/is-there-a-way-in-f-to-type-test-against-a-generic-type-without-specifying-the

open System
open System.Numerics
open Xunit
open Swensen.Unquote

open System.Drawing

// 1- APs in General 

// In F# an active pattern allows to select a brach of execution according to whether some
// input satisfy certain condition(s) - active patterns. 

// 2- APs to choose what to do according to the input and the pattern

// Active Patterns in F# are used to subdivide input data in the same way that matching
// expression are used on discriminated unions. With discriminated union a matched expression
// is used to select a branch according to the value of a discriminated type. With active
// pattern this concept is broaden to inputs that are not necessarily of a discriminated 
// union type for exaple simple types such as interger, strings or even custom types.

// 3- APs to achieve the decomposing of data in mutliple ways

// Sometimes given some data there are mutliple ways this data can be decomposed. For 
// example a unit of measure may be converted or decomposed to several others that is 
// m -> inch , m -> feet, m -> mm or as another example a boat -> (lenght, width) or 
// boat -> (power, volume). Active pattern can be used to define the decomposition of 
// the data accordongly and they can be used later in functions. See the exampled below 
// for a more concrete explanation.

// 4- Partial active patterns

// Active patterns that do not always produce a value are called partial active patterns,
// they have a return value that is an option type. o define a partial active pattern, 
// you use a wildcard character (_) at the end of the list of patterns inside the banana 
// clips. One reason an AP may not be able to produce a value given some data as its input
// might be that the input although being of the expected type fails to match any of the 
// patterns i.e. an AP on a string might be passed a string that does not match any declared
// patterns in which case a PAP would retun the Option value None<T>.

// 5- Disjointed and not disjointed patterns 

// APs and PAPs are normally used to delcare functions that can decompose or tranform the 
// data along a specific execution path depending on the pattern the data is matched to
// by the definition of teh APs or PAPs used in the declaration of the function. However,
// a piece of data needs not match a single pattern that is the pattern need not always be
// mutually exclusive that is disjointed. For example lets name val(||)

// 6- Parameterized Active Patterns

// it is interesting that the signature the |Even|Odd| AP is 
// val (|Even|Odd|):input: int -> Choice<unit,unit>
let (|Even|Odd|) input = if input % 2 = 0 then Even else Odd

[<Fact>]
let ``Active Pattern |Even|Odd|``() = 

    // arrange 
    let Change input = 
        match input with
        | Even -> input * -1
        | Odd -> input + 2
    
    let even = 2
    let odd = 3
    let expectedChangedEven = -2
    let expectedChangedOdd = 5

    // act
    let actualChangedEven = Change even
    let actualChangedOdd = Change odd

    // assert
    test<@ actualChangedEven = expectedChangedEven @>
    test<@ actualChangedOdd = expectedChangedOdd @>

// https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/active-patterns

// Here we defined Active Patterns that know how to decompose Color.
// It is possible to decompose Color in multiple ways hence we can 
// define mutliple Active Patterns.

// val (|RGB|):col: Color -> byte*byte*byte
let (|RGB|) (col : System.Drawing.Color) =
     ( col.R, col.G, col.B )

// val (|HSB|):col: Color -> float32*float32*float32
let (|HSB|) (col : System.Drawing.Color) =
   ( col.GetHue(), col.GetSaturation(), col.GetBrightness() )

[<Fact>]
let``Multiple Active Patterns |RGB|HSB|``() = 

    // arrange
    
    // A function based on a possible Active Pattern for Color.
    // APs are normally used to declare functions by using the "match..with" syntax. 
    let ColorToRGB (col : System.Drawing.Color) = match col with RGB(r,g,b) -> ((float32)r,(float32)g,(float32)b)
    
    // Another function for the same data but a different possible AP.
    // APs are normally used to declare functions by using the "match..with" syntax.
    let ColorToHSB (col : System.Drawing.Color) = match col with HSB(h,s,b) -> (h,s,b)

    // notice that the two function above do nothing else than converting a 3-term tuple  
    // into another 3-term tuple of float32 values.

    // A function that combines all the active patterns.
    // As said sometimes data may be decomposed or converted in multiple ways.
    // In this cases we may employ APs to describe the conversion or decomposotion 
    // algorithms individually. Such AP can be used in isolation or combined into 
    // the definition of functions.
    let ConvertColor col =
        let (r,g,b1) = ColorToRGB col // (255.0f, 0.0f, 0.0f)
        let (h,s,b2) = ColorToHSB col // (0.0, 1.0, 0.5)     
        (r+h,g+s,b1+b2)
        
    let first = (float32)System.Drawing.Color.Red.R + System.Drawing.Color.Red.GetHue();
    let second = (float32)System.Drawing.Color.Red.G + System.Drawing.Color.Red.GetSaturation();
    let third = (float32)System.Drawing.Color.Red.B + System.Drawing.Color.Red.GetBrightness();
    let expected = (first, second, third)

    // act 
    let actual = ConvertColor System.Drawing.Color.Red 

    // assert
    test<@ actual = expected @>

// This is a Partial Active Pattern [PAP].
// The last case in the banana clip of any PAPA is the undercore.
// A PAP always returns an Option type.
// In this PAP the input of type string has a match only if it can
// be parsed to Int32.
// val (|Integer|_|):str:string -> int option
let (|Integer|_|) (str: string) = 
    let mutable intValue = 0
    if System.Int32.TryParse(str, &intValue) then Some(intValue)
    else None

let (|Float|_|) (str: string) = 
    let mutable floatValue = 0.0
    if System.Double.TryParse(str, &floatValue) then Some(floatValue)
    else None


[<Fact>]
let``Partial Active Patterns |Integer|_| and |Float|_|``() = 

    // arrange
    
    // As usual APs and PAPs can be composed into the definition of 
    // a function, normally with the "match..with" pattern. 
    let parseNumeric str = 
        match str with
            | Integer i -> Some(-(float i))
            | Float f -> Some(2.0*f)
            | _ -> None
    
    let i = "1"
    let f = "1.0"
    let s = String.Empty
    let expectedParsedInt = Some(-1.0)
    let expectedParsedFloat = Some(2.0) 
    let expectedNotParsable: Option<float> = None

    // act 
    let actualParsedInt = parseNumeric i
    let actualParsedFloat = parseNumeric f   
    let actualNotParsable = parseNumeric s

    // assert
    test<@ actualParsedInt =  expectedParsedInt @>
    test<@ actualParsedFloat =  expectedParsedFloat @>
    test<@ actualNotParsable = expectedNotParsable @>

// Example of PAPs that are composed into a function whose data 
// might match none, one or either of the pattern. This is the 
// case in which the APs or PAPs are not disjointed as they were 
// in the preceeding examples.

let err = 1.e-10

// Notice that err is declared before x to aid partial application
let isNearlyItegral (err: float) (x: float) = 
    let r = round(x)
    let d = abs(x - r) 
    d < err

let isIntegral = isNearlyItegral err

// PAP that matches an integer to Some(x) when it is a x=n**2 for some n
let (|Square|_|) (x: int) = 
    if (x = 1 || x = 0) then Some(x) 
    elif x < 0 then None
    else
        let y = sqrt (float x)
        if isIntegral y then Some(x)
        else None

// PAP that matches an integer to Some(x) when it is a x=n**3 for some n
let (|Cube|_|) (x: int) =
    if (x = 1 || x = 0 || x = -1) then Some(x) 
    else
        let y = (float x) ** (1.0/3.0)
        if isIntegral y then Some(x)
        else None

// a discriminated union type 
type SCBN = 
        | PW2
        | PW3
        | PW2PW3
        | Neither

[<Fact>]
let``Not Disjointed Partial Active Patterns |Square|_| and |Cube|_|``() =
    
    // arrange
    let isSquare x = match x with             
                    | Square x -> true
                    |_-> false
    
    let isCube x = match x with             
                    | Cube x -> true
                    |_-> false

    let isSquareOrCubeOrBothIntegral x = 
         let isPw2 = isSquare x
         let isPw3 = isCube x
         if(isPw2 && isPw3) then PW2PW3
         elif isPw2 then PW2
         elif isPw3 then PW3
         else Neither
    
    // act
    let actualNeither = isSquareOrCubeOrBothIntegral 3
    let actualSquare = isSquareOrCubeOrBothIntegral 4
    let actualCube = isSquareOrCubeOrBothIntegral 27 
    let actualBoth = isSquareOrCubeOrBothIntegral 64 
    let actualWithZero = isSquareOrCubeOrBothIntegral 0 
    let actualWithOne = isSquareOrCubeOrBothIntegral 1
    let actualWithMinusOne = isSquareOrCubeOrBothIntegral -1
    let actualWithNegative = isSquareOrCubeOrBothIntegral -19

    // assert
    test<@ actualNeither = Neither @>
    test<@ actualSquare = PW2 @>
    test<@ actualCube = PW3 @>
    test<@ actualBoth = PW2PW3 @>
    test<@ actualWithZero = PW2PW3 @>
    test<@ actualWithOne = PW2PW3 @>
    test<@ actualWithMinusOne = PW3 @>
    test<@ actualWithNegative = Neither @>



