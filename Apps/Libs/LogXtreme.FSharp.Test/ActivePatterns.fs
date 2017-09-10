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
// expression are used on discriminated unions. With discriminated union a match expression
// is used to select a branch according to the value of a discriminated type. With active
// pattern this concept is broaden to inputs that are not necessarily of a discriminated 
// union type.

// 3- APs tp decomposing data in mutliple ways

// Sometimes given some data there are mutliple ways this data can be decomposed. For 
// example a unit of measure may be converted or decomposed to several others that is 
// m -> inch , m -> feet, m -> mm or as another example a boat -> (lenght, width) or 
// boat -> (power, volume). Active pattern can be used to define the decomposition of 
// the data accordongly and they can be used later in functions. See the example below 
// for a concrete reference.

// 4- Partial active patterns

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

// Here we defined Active Patterns that know hot to decompose Color
// It is possible to decompose Color in multiple ways hence we can 
// define mutliple Active Patterns

let (|RGB|) (col : System.Drawing.Color) =
     ( col.R, col.G, col.B )

let (|HSB|) (col : System.Drawing.Color) =
   ( col.GetHue(), col.GetSaturation(), col.GetBrightness() )



[<Fact>]
let``Multiple Active Patterns |RGB|HSB|``() = 

    // arrange
    
    // a function based on a possible Active Pattern for Color
    let ColorToRGB (col : System.Drawing.Color) = match col with RGB(r,g,b) -> ((float32)r,(float32)g,(float32)b)
    
    // another function for the same data but a different possible AP
    let ColorToHSB (col : System.Drawing.Color) = match col with HSB(h,s,b) -> (h,s,b)

    // notice that the two function above do nothing else than converting a 3-tuple to 
    // a list

    // a function that combines all teh active patterns
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
