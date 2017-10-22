module LogXtreme.FSharp.UtilsFunctions

open Microsoft.FSharp.Quotations
open Microsoft.FSharp.Quotations.Patterns

// Efficient concatenation of objects into a string with string builder
// https://stackoverflow.com/questions/18595597/is-using-a-stringbuilder-a-right-thing-to-do-in-f
let strconc = 
    fun (data) ->         
        let sb = new System.Text.StringBuilder()
        for o in data do sb.Append(o.ToString()) |> ignore
        sb.ToString()

// examples
strconc ["one"; "two"] |> printfn "%s"
strconc [1;2;3] |> printfn "%s"
strconc (["one"; "two"; 3 ]: list<obj>) |> printfn "%s"


// Quotation to read the property name...
// http://www.contactandcoil.com/software/dotnet/getting-a-property-name-as-a-string-in-f/
let propertyName quotation = 
    match quotation with 
    | PropertyGet (_,propertyInfo,_) -> propertyInfo.Name
    | _ -> System.String.Empty

//------------------------------------------------------------------------
// Generate prime numbers up to a max integer
// https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/lists
//------------------------------------------------------------------------
// if n is an integer then n = sqrt(n)^2 hence the integer round(sqrt(n))
// is also the largest possible factor for any integer number in the range 
// 1 to n. In other words any interger number in the range 1 to n can only 
// have factors between 2 and round(sqtr(n)). 1 does not count because any
// integer has 1 as divisor including primes.
let getPrimesUpTo n = 

    // pluck a number from [2,round(sqrt(n))] and then go through the xs in [1..n]
    // and test whether x has any factors in [2,round(sqrt(n))] if x happens to be 
    // any of the numbers in [2,round(sqrt(n))] then keep it as it will be tested 
    // for factors in [2,round(sqrt(n))] smaller than itself.
    let predicate head x = head = x || x % head <> 0

    // remove from list any number with factors in factors
    let rec removeMultiples factors list =
        match factors with
            | head::tail -> removeMultiples tail (List.filter (predicate head)  list)
            | [] -> list

    let max = int(sqrt(float n))
    removeMultiples [2..max] [2..n]

    //-------------------------------------------------------------------------------------------------------
    // Reversing a string
    // https://stackoverflow.com/questions/4556160/is-there-more-simple-or-beautiful-way-to-reverse-a-string
    // https://stackoverflow.com/questions/13085575/some-basic-seq-and-list-questions
    //-------------------------------------------------------------------------------------------------------
       
